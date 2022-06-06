using CFR.LZ;
using UnityEngine;
using CFR.SHIP;
using CFR.CARGO;
using System.Collections;
using CFR.INPUT;
using CFR.STAGE;

namespace CFR.UI
{
    public class LZManifestDisplayManager : ManifestDisplayManager
    {
        #region//Cached Components
        Canvas myCanvas;
        [SerializeField] RectTransform box = null;
        [SerializeField] GameObject transferUI = null;
        [SerializeField] Transform typeHelpBoxContainer = null;
        ActiveLZController activeLZController;
        Manifest shipManifest;
        ShipMoveSM shipSM;
        PlayerInputManager playerInputs;
        AudioSource sfxPlayer;
        ShipController shipController;
        StageManager stageManager;
        #endregion

        #region//Activation variables
        [SerializeField] Vector2 leftPos = new Vector2();
        [SerializeField] Vector2 centerPos = new Vector2();
        [SerializeField] Vector2 rightPos = new Vector2();
        [SerializeField] float yPosWideScreen = -5f;
        [SerializeField] float scaleWideScreen = 1.9f;
        float defaultScale;
        bool hidden = true;
        #endregion


        #region//Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            isActive = false;
            sfxPlayer = GetComponentInChildren<AudioSource>();
            shipSM = FindObjectOfType<ShipMoveSM>();
            myCanvas = GetComponent<Canvas>();
            activeLZController = FindObjectOfType<ActiveLZController>();
            shipManifest = GameObject.FindGameObjectWithTag("ShipManifest").GetComponent<Manifest>();
            playerInputs = FindObjectOfType<PlayerInputManager>();
            shipController = FindObjectOfType<ShipController>();
            stageManager = FindObjectOfType<StageManager>();
            defaultScale = box.localScale.x;
        }

        protected override void Start()
        {
            base.Start();
            ShutDown();
        }

        protected override void Update()
        {
            if (!isActive) return;
            CheckPeekHideButtonHold();

            if (hidden) return;
            base.Update();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            activeLZController.LZControllerStateChange += StateChange;
            ShipMoveSM.stateChanged += ChangeVisabilityAfterShipStateChange;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            activeLZController.LZControllerStateChange -= StateChange;
            ShipMoveSM.stateChanged -= ChangeVisabilityAfterShipStateChange;
        }
        #endregion

        #region//StartUp/ShutDown
        public void StartUp()
        {
            isActive = true;
            SetScreenPosition(activeLZController.myLZ);
            listManager.UpdateList();
            ResetUI();
        }

        public void ShutDown()
        {
            isActive = false;
            hidden = true;
            myCanvas.enabled = false;
            RefreshUI();
        }
        #endregion

        #region//Modify list
        protected override void SetItem(ActiveItemDisplay _display, int _itemNo, bool _setActive)
        {
            if(_itemNo < myManifest.GetMaxSize())
                _display.UpdateItem(myManifest.GetItem(_itemNo));
            else
                _display.UpdateItem(shipManifest.GetItem(_itemNo - myManifest.GetMaxSize()));

            if(!_setActive) return;
            if(activeItemSet) listManager.UnhighlightListItem(activeItem);
            listManager.HighlightListItem(_itemNo, _setActive);
            activeItem = _itemNo;
        }

        void SetScreenPosition(LandingZone _lz)
        {
            if (!_lz.isMid)
            {
                box.anchoredPosition = centerPos;
                MoveHelpBoxes(true);
            }
            else if (Camera.main.WorldToScreenPoint(_lz.transform.position).x > Screen.width / 2)
            {
                box.anchoredPosition = leftPos;
                MoveHelpBoxes(true);
            }
            else
            {
                box.anchoredPosition = rightPos;
                MoveHelpBoxes(false);
            }

            if(Camera.main.aspect > (16f/9f))
            {
                box.anchoredPosition = new Vector2(box.anchoredPosition.x, yPosWideScreen);
                box.localScale = new Vector2(scaleWideScreen, scaleWideScreen);
            }
            else
            {
                box.localScale = new Vector2(defaultScale, defaultScale);
            }
        }

        void MoveHelpBoxes(bool _displayOnRightside)
        {
            Vector3 position = typeHelpBoxContainer.localPosition;
            position.x = Mathf.Abs(position.x);
            position.x *= _displayOnRightside ? 1 : -1;
            typeHelpBoxContainer.localPosition = position;
        }
        #endregion
    
        #region //Activation
        void StateChange(bool _canTransfer)
        {
            if (_canTransfer)
            {
                StartUp();
            }
            else
            {
                ShutDown();
            }
        }

        void ChangeVisabilityAfterShipStateChange(ShipMoveState _newState)
        {
            if(_newState is MovingState)
            {
                Hide(true);
                transferUI.SetActive(true);
            }
            else
            {
                Hide(false);
                transferUI.SetActive(false);
            }
        }
        #endregion

        #region //Hide
        private void CheckPeekHideButtonHold()
        {
            bool hide = !(playerInputs.shipSystem.lzWindowInput ^ shipSM.IsLanded());
            if (hide != hidden)
            {
                Hide(!hidden);
            }
        }

        void Hide(bool _hide)
        {
            hidden = _hide || !isActive;
            sfxPlayer.enabled = !hidden;
            myCanvas.enabled = !hidden;
            activeLZController.SetActive(!hidden);
        }
        #endregion
    }
}