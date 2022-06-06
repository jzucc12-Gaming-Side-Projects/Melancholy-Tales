using CFR.INPUT;

namespace CFR.SHIP
{
    public abstract class ShipCargoState : ShipState
    {
        protected ShipCargoSM sm;

        public ShipCargoState(ShipCargoSM _sm, PlayerInputManager _im, ShipController _controller)
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