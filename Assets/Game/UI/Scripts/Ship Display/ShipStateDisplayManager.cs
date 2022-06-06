using CFR.CARGO;
using CFR.SHIP;
using JZ.MENU;
using UnityEngine;

namespace CFR.UI
{
    public class ShipStateDisplayManager : MonoBehaviour
    {
        #region//Text displays
        [Header("Text displays")]
        [SerializeField] TextDisplay flightModeText = null;
        [SerializeField] TextDisplay transferText = null;
        [SerializeField] TextDisplay timesLandedText = null;
        Manifest shipManifest;
        ShipMoveSM shipSM;
        #endregion

        #region//Stats
        int timesLanded = 0;
        #endregion


        #region//Monobehaviour
        private void Awake()
        {
            shipManifest = GetComponentInParent<Manifest>();
            shipSM = FindObjectOfType<ShipMoveSM>();
        }

        private void OnEnable()
        {
            ShipMoveSM.stateChanged += UpdateMovementMode;
        }

        private void OnDisable()
        {
            ShipMoveSM.stateChanged -= UpdateMovementMode;
        }
        #endregion

        #region//Updating Stat/Display Methods
        void UpdateMovementMode(ShipMoveState _newState)
        {
            if (_newState is MovingState)
            {
                flightModeText.SetText("In Flight");
                if(shipManifest.GetMaxSize() >= 2) transferText.SetText("Within Ship");
                else transferText.SetText("Disabled");
            }
            else if (_newState is LandingState)
            {
                flightModeText.SetText("Landed");
                timesLandedText.SetText("Landings: " + ++timesLanded);
                transferText.SetText("With Landing Zone");
            }
        }

        public void ItineraryUpdate(int _takeOffs, bool _atEnd)
        {
            if(!_atEnd || shipSM.heldState is MovingState)
            {
                flightModeText.SetText("In Flight");
                if(shipManifest.GetMaxSize() >= 2) transferText.SetText("Within Ship");
                else transferText.SetText("Disabled");
                int landingValue = _takeOffs - (_atEnd ? 1 : 0);
                timesLandedText.SetText("Landings: " + landingValue.ToString());
            }
            else
            {
                flightModeText.SetText("Landed");
                transferText.SetText("With Landing Zone");
                timesLandedText.SetText("Landings: " + _takeOffs.ToString());
            }
        }
        #endregion
    }

}