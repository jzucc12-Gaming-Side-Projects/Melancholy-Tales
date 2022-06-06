using CFR.INPUT;
using CFR.SHIP;
using CFR.STAGE;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace CFR.ITINERARY
{
    public class ItineraryInputManager : MonoBehaviour
    {
        [SerializeField] bool cantUseThisStage = false;
        [SerializeField] GameObject defeatMenuButton = null;
        StageInfo unlockPrereq;
        ShipMoveSM shipSM;

        #region //Events
        [SerializeField] public UnityEvent ActivationEvent = null;
        [SerializeField] public UnityEvent DeactivationEvent = null;
        #endregion

        #region //Input Variables
        private ItineraryInputs inputs;
        private InputAction moveAction;
        private InputAction selectAction;
        private InputAction toggleAction;
        #endregion

        #region //State Variables
        private int move = 0;
        private bool selected = false;
        private bool itineraryActive = false;
        private bool defeatState = false;
        bool isActive = false;
        #endregion


        #region //Monobehaviour
        private void Awake() 
        {
            inputs = new ItineraryInputs();
            moveAction = inputs.Itinerary.Move;
            selectAction = inputs.Itinerary.Select;
            toggleAction = inputs.Itinerary.Toggle;
            shipSM = FindObjectOfType<ShipMoveSM>();
            unlockPrereq = Resources.Load<StageInfo>("Stages/Log 2/2-1");
        }

        private void OnEnable()
        {
            StageInputManager.OnPause += Paused;
            ActivateToggle();
            isActive = true;

            if(cantUseThisStage && !unlockPrereq.HasCompleted())
            {
                gameObject.SetActive(false);
                defeatMenuButton.SetActive(false);
            }
        }

        private void OnDisable()
        {
            StageInputManager.OnPause -= Paused;
            DeactivateToggle();
            isActive = false;
            DeactivateUsage();
        }
        #endregion

        #region //Activation
        //Public
        public void ActivateItinerary(bool _activate)
        {
            if(shipSM.lockState) return;

            if(_activate)
            {
                ActivationEvent?.Invoke();
                ActivateUsage();
            }
            else
            {
                DeactivationEvent?.Invoke();
                DeactivateUsage();
            }

            itineraryActive = _activate;
        }

        public void ActivateUsage()
        {
            moveAction.Enable();
            selectAction.Enable();
            moveAction.started += OnMoveAction;
            selectAction.started += OnSelectAction;
        }

        public void DeactivateUsage()
        {
            moveAction.Disable();
            selectAction.Disable();
            moveAction.started -= OnMoveAction;
            selectAction.started -= OnSelectAction;
        }

        public void ActivateToggle()
        {
            if(isActive) return;
            toggleAction.Enable();
            toggleAction.started += OnToggleAction;
            isActive = true;
        }

        public void DeactivateToggle()
        {
            if(!isActive) return;
            toggleAction.Disable();
            toggleAction.started -= OnToggleAction;
            isActive = false;
        }
        #endregion

        #region//Input Callbacks
        private void OnMoveAction(InputAction.CallbackContext context)
        {
            move = (int)Mathf.Sign(context.ReadValue<float>());
        }

        private void OnSelectAction(InputAction.CallbackContext context)
        {
            selected = true;
        }

        private void OnToggleAction(InputAction.CallbackContext context)
        {
            ActivateItinerary(!itineraryActive);
        }
        #endregion

        #region//Getters and Expenders
        public bool GetItineraryActive() { return itineraryActive; }
        public void ExpendMove() { move = 0; }
        public void ExpendSelect() { selected = false; }
        public int GetMove() { return move; }
        public bool GetDidMove() { return move != 0; }
        public bool GetSelected() { return selected; }
        public bool GetDefeatState() { return defeatState; }
        #endregion
    
        private void Paused(bool _pausing)
        {
            if(_pausing)
            {
                DeactivateToggle();
                DeactivateUsage();
            }
            else
            {
                ActivateToggle();
                if(itineraryActive)
                    ActivateUsage();
            }
        }
    
        public void SetDefeatState(bool _defeated) { defeatState = _defeated; }

        public void LockInputs(bool _lock) 
        { 
            if(_lock) enabled = false;
            else enabled = true;
        }
    }
}