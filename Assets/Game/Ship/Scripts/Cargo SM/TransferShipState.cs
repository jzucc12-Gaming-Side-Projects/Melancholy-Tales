using CFR.CARGO;
using CFR.INPUT;
using JZ.CORE;
using UnityEngine;

namespace CFR.SHIP
{
    public class TransferShipState : ShipCargoState
    {
        ManifestNavigator myNavigator;
        public TransferShipState(ShipCargoSM _sm, PlayerInputManager _im, ShipController _controller) : base(_sm, _im, _controller) 
        { 
            myNavigator = sm.navigator;
        }

        #region//State methods
        public override void StartState()
        {
            myNavigator.StartUp();
            ShipMoveSM.stateChanged += ChangeState;
        }

        public override void EndState(State _nextState)
        {
            myNavigator.Shutdown();
            ShipMoveSM.stateChanged -= ChangeState;
        }

        public override void StateUpdate()
        {
            if(controller.LZController.isActive)
            {
                myNavigator.ResetItem();
                return;
            }
            myNavigator.Navigation();
            if(sm.myManifest.GetMaxSize() < 2) return;
            myNavigator.Selection();
            SwapCheck();
        }

        void ChangeState(ShipMoveState _)
        {
            if(sm.moveSM.IsMoving()) return;
            sm.ChangeState(sm.openState);
        }
        #endregion

        #region//Item Methods
        private void SwapCheck()
        {
            if (inputManager.shipSystem.transferInput)
            {
                inputManager.shipSystem.ExpendTransferInput();

                if (myNavigator.activeItemSet)
                {
                    if(myNavigator.arrow == myNavigator.activeItem) return; 
                    int firstItemNo = myNavigator.activeItem;
                    int secondItemNo = myNavigator.arrow;
                    CargoItem firstItem = sm.myManifest.GetItem(firstItemNo);
                    CargoItem secondItem = sm.myManifest.GetItem(secondItemNo);

                    if(!sm.myManifest.TrySwap(firstItem, secondItemNo)) 
                    {
                        sm.SwapOcurred(false);
                        return;
                    }
                    if(!sm.myManifest.TrySwap(secondItem, firstItemNo))
                    {
                        sm.SwapOcurred(false);
                        return;
                    }

                    sm.myManifest.SwapItems(firstItemNo, secondItemNo, sm.myManifest);
                    myNavigator.ResetItem();
                    sm.SwapOcurred(true);
                }
            }
        }
        #endregion
    }
}