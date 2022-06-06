using CFR.ITINERARY;
using CFR.LZ;
using CFR.SHIP;
using UnityEngine;

namespace CFR.UI
{
    public class ShipUI : MonoBehaviour
    {
        #region //Cached components
        [SerializeField] GameObject landButtonUI = null;
        [SerializeField] GameObject peekLZWindowButtonUI = null;
        [SerializeField] GameObject hideLZWindowButtonUI = null;
        ShipController shipController;
        ShipMoveSM shipSM;
        ItineraryInputManager itineraryInputs;
        #endregion

        bool itineraryActive = false;
        bool generalShowConditions = true;
        bool victory = false;


        #region //Monobehaviour
        private void Awake() 
        {
            shipController = GetComponent<ShipController>();
            shipSM = GetComponent<ShipMoveSM>();
            itineraryInputs = FindObjectOfType<ItineraryInputManager>();
        }

        private void OnEnable() 
        {
            itineraryInputs.ActivationEvent.AddListener(ItineraryActivate);
            itineraryInputs.DeactivationEvent.AddListener(ItineraryDeacitvate);
            LandingZone.Victory += Victory;
        }

        private void OnDisable() 
        {
            itineraryInputs.ActivationEvent.RemoveListener(ItineraryActivate);
            itineraryInputs.DeactivationEvent.RemoveListener(ItineraryDeacitvate);    
            LandingZone.Victory -= Victory;
        }

        private void Update() 
        {
            if(victory) return;
            generalShowConditions = !itineraryActive && shipController.overLandingZone;
            LandButtonVisibility();
            LZWindowButtonVisibility();
        }
        #endregion

        #region //UI Showing
        private void LandButtonVisibility()
        {
            bool specificShow = shipSM.IsMoving();
            landButtonUI.SetActive(generalShowConditions && specificShow);
        }

        private void LZWindowButtonVisibility()
        {
            peekLZWindowButtonUI.SetActive(generalShowConditions && !shipSM.IsLanded());
            hideLZWindowButtonUI.SetActive(generalShowConditions && shipSM.IsLanded());
        }

        private void ItineraryActivate()
        {
            itineraryActive = true;
        }

        private void ItineraryDeacitvate()
        {
            itineraryActive = false;
        }

        private void Victory()
        {
            victory = true;
            landButtonUI.SetActive(false);
            peekLZWindowButtonUI.SetActive(false);
            hideLZWindowButtonUI.SetActive(false);
        }
        #endregion
    }

}