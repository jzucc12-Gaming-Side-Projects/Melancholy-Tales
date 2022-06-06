using CFR.CARGO;
using CFR.INPUT;
using JZ.AUDIO;
using System;

namespace CFR.SHIP
{
    public class ShipCargoSM : ShipStateMachine<ShipCargoState>, INavigationController
    {
        #region//Available states
        public OpenCargoState openState { get; private set; }
        public TransferShipState transferShipState { get; private set; }
        #endregion

        #region//Cached variables
        public Manifest myManifest { get; private set; }
        public ShipMoveSM moveSM {get; private set; }
        public ManifestNavigator navigator { get; private set; }
        //public static event Action OnSwap;
        #endregion


        #region//Monobehaviour
        protected override void Awake()
        {
            base.Awake();
            moveSM = FindObjectOfType<ShipMoveSM>();
            myManifest = GetComponent<Manifest>();
        }

        protected override void Start()
        {
            navigator = new ManifestNavigator(inputManager, true, myManifest.GetMaxSize());
            base.Start();
        }
        #endregion

        #region//Start up
        protected override void StateStartUp()
        {
            openState = new OpenCargoState(this, inputManager, controller);
            transferShipState = new TransferShipState(this, inputManager, controller);
            ChangeState(transferShipState);
        }
        #endregion

        #region//Navigation Controller
        public ManifestNavigator GetNavigator()
        {
            return navigator;
        }
        #endregion

        public void SwapOcurred(bool _swapped)
        {
            if(_swapped) 
            {
                GetComponent<AudioManager>().Play("Swap Success");
                //OnSwap?.Invoke();
            }
            else
            {
                GetComponent<AudioManager>().Play("Swap Fail");
            }
        }
    }
}
