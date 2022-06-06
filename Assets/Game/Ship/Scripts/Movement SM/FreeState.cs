using CFR.INPUT;
using UnityEngine;

namespace CFR.SHIP
{
    public class FreeState : ShipMoveState
    {
        #region//State Methods
        public FreeState(ShipMoveSM _sm, PlayerInputManager _im, ShipController _controller) : base(_sm, _im, _controller) 
        { 

        }
        #endregion
    }
}
