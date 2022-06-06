using CFR.CARGO;
using CFR.INPUT;
using JZ.CORE;
using UnityEngine;

namespace CFR.SHIP
{
    public class OpenCargoState : ShipCargoState
    {
        ManifestNavigator theirNavigator;
        public OpenCargoState(ShipCargoSM _sm, PlayerInputManager _im, ShipController _controller) : base(_sm, _im, _controller) 
        { 
            theirNavigator = controller.LZController.navigator;
        }


        #region//State methods
        public override void StartState()
        {
            ShipMoveSM.stateChanged += ChangeState;
        }

        public override void EndState(State _nextState)
        {
            ShipMoveSM.stateChanged -= ChangeState;
        }

        public override void StateUpdate()
        {
            SwapCheck();
        }

        void ChangeState(ShipMoveState _)
        {
            if(sm.moveSM.IsLanded()) return;
            sm.ChangeState(sm.transferShipState);
        }
        #endregion

        #region//Item methods
        private void SwapCheck()
        {
            if (inputManager.shipSystem.transferInput)
            {
                inputManager.shipSystem.ExpendTransferInput();
                if (!controller.LZController.isActive) return;

                if (theirNavigator.activeItemSet)
                {
                    if(theirNavigator.arrow == theirNavigator.activeItem) return; 
                    Manifest theirManifest = controller.currentLZ.GetManifest();
                    Manifest firstManifest = (theirNavigator.activeItem < theirManifest.GetMaxSize() ? theirManifest : sm.myManifest);
                    Manifest secondManifest = (theirNavigator.arrow < theirManifest.GetMaxSize() ? theirManifest : sm.myManifest);
                    
                    int firstItemNo = theirNavigator.activeItem;
                    if (firstItemNo >= theirManifest.GetMaxSize()) firstItemNo -= theirManifest.GetMaxSize();

                    int secondItemNo = theirNavigator.arrow;
                    if(secondItemNo >= theirManifest.GetMaxSize()) secondItemNo -= theirManifest.GetMaxSize();

                    CargoItem firstItem = firstManifest.GetItem(firstItemNo);
                    CargoItem secondItem = secondManifest.GetItem(secondItemNo);
                    
                    if(!firstManifest.TrySwap(secondItem, firstItemNo)) 
                    {
                        sm.SwapOcurred(false);
                        return;
                    }
                    if(!secondManifest.TrySwap(firstItem, secondItemNo)) 
                    {
                        sm.SwapOcurred(false);
                        return;
                    }

                    firstManifest.SwapItems(firstItemNo, secondItemNo, secondManifest);
                    theirNavigator.ResetItem();
                    sm.SwapOcurred(true);
                }
            }
        }
        #endregion
    }
}