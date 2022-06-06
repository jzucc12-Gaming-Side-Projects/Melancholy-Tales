using CFR.INPUT;
using JZ.CORE;

namespace CFR.SHIP
{
    public abstract class ShipState : State
    {
        protected PlayerInputManager inputManager;
        protected ShipController controller;
    }
}