//using JZ.CORE;
//using UnityEngine;

//namespace CFR.CARGO
//{
//    public class TransferLZState : ShipCargoState
//    {
//        #region//Selection variables
//        int itemShowing = -1;
//        #endregion


//        //Possibly make this a child state of a common parent with open or transfer ship... It's kind of both of them...////////////////////////////////////////////////////////

//        #region//State methods
//        public TransferLZState(ShipCargoSM _sm, ShipInputManager _im, ShipController _controller) : base(_sm, _im, _controller) { }

//        public override void StartState()
//        {
//            itemShowing = sm.openState.GetActiveItem();
//        }

//        public override void EndState(State _nextState)
//        {
//            inputManager.ExpendManifestInput(true);
//            inputManager.ExpendManifestInput(false);
//        }

//        public override void StateUpdate()
//        {
//            SelectShipItem();
//            SwapCheck();
//            StateChangeCheck();
//        }

//        protected override void StateChangeCheck()
//        {
//            if (!StateChangePreCheck()) return;
//            if (!inputManager.transferInput) return;

//            inputManager.ExpendTransferInput();
//            sm.ChangeState(sm.openState);
//        }
//        #endregion

//        #region//Getters
//        public int GetActiveItem() { return itemShowing; }
//        #endregion

//        #region//Item methods
//        void ResetItem(bool _shipItem)
//        {
//            if (_shipItem && itemShowing != -1)
//            {
//                sm.singleDisplay.UpdateItem(null, HoldType.normal);
//                sm.listManager.UnhighlightListItem(itemShowing);
//                itemShowing = -1;
//            }
//            else if(!_shipItem && sm.lzDisplay.activeItem != -1)
//                sm.lzDisplay.ResetItem();
//        }

//        void SetShipItem(int _itemNo)
//        {
//            if (itemShowing != -1) sm.listManager.UnhighlightListItem(itemShowing);
//            sm.singleDisplay.UpdateItem(sm.myManifest.GetItem(_itemNo), sm.myManifest.GetHoldType(_itemNo));
//            sm.listManager.HighlightListItem(_itemNo, true);
//            itemShowing = _itemNo;
//        }

//        private void SelectShipItem()
//        {
//            if (inputManager.shipManifestPressed && inputManager.ValidManifestInput(sm.myManifest.GetMaxSize(), true))
//            {
//                int itemNo = inputManager.shipManifestInput;

//                if (itemShowing != itemNo)
//                    SetShipItem(itemNo);
//                else
//                    ResetItem(true);

//                inputManager.ExpendManifestInput(true);
//            }
//        }

//        private void SwapCheck()
//        {
//            if (inputManager.interactInput)
//            {
//                if (itemShowing != -1 && sm.lzDisplay.activeItem != -1)
//                {
//                    sm.myManifest.SwapItems(itemShowing, sm.lzDisplay.activeItem, sm.lzDisplay.myManifest);
//                    sm.listManager.UpdateList();
//                    sm.lzDisplay.UpdateList();
//                    ResetItem(true);
//                    ResetItem(false);
//                }
//                else
//                {
//                    //Cancelling sound effect
//                }

//                inputManager.ExpendInteractInput();
//            }
//        }
//        #endregion
//    }
//}