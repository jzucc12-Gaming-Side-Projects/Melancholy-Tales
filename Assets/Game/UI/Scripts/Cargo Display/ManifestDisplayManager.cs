using CFR.CARGO;
using CFR.INPUT;
using CFR.SHIP;
using UnityEngine;

namespace CFR.UI
{
    public class ManifestDisplayManager : MonoBehaviour
    {
        #region//Cached variables
        [SerializeField] ActiveItemDisplay firstDisplay = null;
        [SerializeField] ActiveItemDisplay secondDisplay = null;
        [SerializeField] protected ManifestListManager listManager = null;
        ManifestNavigator navigator;
        protected Manifest myManifest;
        protected bool isActive = true;
        #endregion

        #region//Display variables
        int arrowPos = -1;
        protected int activeItem = -99;
        protected bool activeItemSet => activeItem != -99;
        #endregion


        #region//Monobehaviour
        protected virtual void Awake()
        {
            myManifest = GetComponent<Manifest>();
        }

        protected virtual void Start()
        {
            navigator = GetComponent<INavigationController>().GetNavigator();
            if(GetComponent<CargoTransferer>() && myManifest.GetMaxSize() < 2)
                secondDisplay.gameObject.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            CargoTransferer.OnSwap += RefreshUI;
            myManifest.ItineraryChanged += RefreshUI;
        }

        protected virtual void OnDisable()
        {
            CargoTransferer.OnSwap -= RefreshUI;
            myManifest.ItineraryChanged -= RefreshUI;
        }

        protected virtual void Update()
        {
            if(!isActive) return;
            UpdateArrow();
            UpdateSelections();
        }
        #endregion

        #region//Updating from navigator
        public void RefreshUI()
        {
            if(navigator == null) return;
            ResetSelectedItem();
            UpdateArrow();
            SetItem(firstDisplay, arrowPos, false);
        }

        protected void ResetUI()
        {
            if(navigator == null) return;
            arrowPos = 0;
            RefreshUI();
        }

        void UpdateArrow()
        {
            if (navigator.arrow == arrowPos) return;
            SetArrow(navigator.arrow);
            SetItem(firstDisplay, navigator.arrow, false);
        }

        void UpdateSelections()
        {
            if(!(navigator.activeItemSet ^ activeItemSet)) return;

            if(navigator.activeItemSet)
            {
                secondDisplay.gameObject.SetActive(true);
                SetItem(secondDisplay, navigator.activeItem, true);
            }
            else
            {
                ResetSelectedItem();
            }
        }
        #endregion

        #region//Setting and resetting
        protected virtual void SetItem(ActiveItemDisplay _display, int _newItem, bool _setActive)
        {
            if(arrowPos < 0)
                _display.UpdateItem(myManifest.GetItem(0));
            else
                _display.UpdateItem(myManifest.GetItem(_newItem));

            if(!_setActive) return;
            if(activeItemSet) listManager.UnhighlightListItem(activeItem);
            listManager.HighlightListItem(_newItem, _setActive);
            activeItem = _newItem;
        }

        protected void ResetSelectedItem()
        {
            if(activeItemSet) listManager.UnhighlightListItem(activeItem);
            secondDisplay.gameObject.SetActive(false);
            activeItem = -99;
        }

        void SetArrow(int _arrow)
        {
            if(arrowPos >= 0) listManager.SetArrow(arrowPos, false);
            if(_arrow >= 0) listManager.SetArrow(_arrow, true);
            arrowPos = _arrow;
        }
        #endregion
    }
}