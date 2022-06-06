using CFR.INPUT;
using JZ.CORE;

namespace CFR.SHIP
{
    public abstract class ShipMoveState : ShipState
    {
        protected ShipMoveSM sm;

        public ShipMoveState(ShipMoveSM _sm, PlayerInputManager _im, ShipController _controller)
        {
            sm = _sm;
            inputManager = _im;
            controller = _controller;
        }

        protected override bool StateChangePreCheck()
        {
            if (sm.lockState) return false;
            return true;
        }
    }
}
