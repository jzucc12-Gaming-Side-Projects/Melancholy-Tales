using CFR.INPUT;
using JZ.CORE;

namespace CFR.SHIP
{
    public abstract class ShipStateMachine<T> : MyStateMachine<T> where T : ShipState 
    {
        #region//Core variables
        protected ShipController controller;
        protected PlayerInputManager inputManager;
        #endregion


        #region//Monobehaviour
        protected virtual void Awake()
        {
            controller = FindObjectOfType<ShipController>();
            inputManager = FindObjectOfType<PlayerInputManager>();
        }

        protected virtual void OnEnable()
        {
            stateChanged += StateChange;
        }

        protected virtual void OnDisable()
        {
            stateChanged -= StateChange;
        }

        void StateChange(T _newState)
        {
            inputManager.ReinitializeInputs();
        }
        #endregion
    }
}