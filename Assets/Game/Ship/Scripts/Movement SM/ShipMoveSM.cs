using CFR.ITINERARY;

namespace CFR.SHIP
{
    public class ShipMoveSM : ShipStateMachine<ShipMoveState>, IItineraryElement
    {
        #region//Available States
        public MovingState movingState { get; private set; }
        public LandingState landState { get; private set; }
        public FreeState freeState { get; private set; }
        public ShipMoveState heldState {get; private set; }
        #endregion

        #region //Monobehaviour
        protected override void OnEnable()
        {
            base.OnEnable();
            inputManager.LandedCheck += IsLanded;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            inputManager.LandedCheck -= IsLanded;
        }
        protected override void Update()
        {
            base.Update();
        }
        #endregion

        #region//Start up
        protected override void StateStartUp()
        {
            movingState = new MovingState(this, inputManager, controller);
            landState = new LandingState(this, inputManager, controller);
            freeState = new FreeState(this, inputManager, controller);
            ChangeState(movingState);
        }
        #endregion

        public bool IsFree() { return currentState == freeState; }
        public bool IsMoving() { return currentState == movingState; }
        public bool IsLanded() { return currentState == landState; }

        #region //IItinerary Element
        public void SaveState()
        {
            //Not needed
        }

        public void HoldCurrentState()
        {
            heldState = currentState;
        }

        public void RestoreCurrentState()
        {
            if(currentState != heldState)
            {
                ChangeState(freeState);
                ChangeState(heldState);
            }
        }

        public void ShowState(int _index)
        {
            //Not needed
        }

        public void DeleteOldStates(int _limit)
        {
            //Not needed
        }

        public void RevertHeldState(int _index)
        {
            heldState = movingState;
        }
        #endregion
    }
}