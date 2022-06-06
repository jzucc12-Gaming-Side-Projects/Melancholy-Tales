using JZ.INPUT;
using UnityEngine.InputSystem;
using UnityEngine;

namespace CFR.INPUT
{
    public class ShipInputSystem : MyInputSystem
    {
        #region//Input actions
        public InputAction movementAction { get; private set; }
        public InputAction landAction { get; private set; }
        public InputAction transferAction { get; private set; }
        public InputAction lzWindowAction { get; private set; }
        #endregion

        #region//Inputs
        public bool landInput { get; private set; }
        public bool moveInput { get; private set; }
        public bool transferInput { get; private set; }
        public bool lzWindowInput { get; private set; }
        #endregion

        #region //Frame buffer
        int transferFrameBuffer = 3;
        int currentBuffer = 0;
        #endregion


        #region//Constructor
        public ShipInputSystem(IInputActionCollection _inputs) : base(_inputs)
        {
            var shipInputs = (ShipInputs)_inputs;
            var map = shipInputs.Player;
            movementAction = map.Move;
            landAction = map.Land;
            transferAction = map.Transfer;
            lzWindowAction = map.LZWindow;
        }
        #endregion

        #region//Startup shutdown
        public override void InitializeInputs()
        {
            landInput = false;
            moveInput = false;
            transferInput = false;
            lzWindowInput = false;
        }

        protected override void SubscribeEvents(bool _startUp)
        {
            if (_startUp)
            {
                landAction.started += OnLandInput;
                transferAction.started += OnTransferInput;
                lzWindowAction.started += OnLZWindowInput;
                lzWindowAction.canceled += ExpendLZWindowInput;
            }
            else
            {
                landAction.started -= OnLandInput;
                transferAction.started -= OnTransferInput;
                lzWindowAction.started -= OnLZWindowInput;
                lzWindowAction.canceled -= ExpendLZWindowInput;
            }
        }

        protected override void EnableActions(bool _startUp)
        {
            currentBuffer = 0;
            if (_startUp)
            {
                transferAction.Enable();
                movementAction.Enable();
                landAction.Enable();
                lzWindowAction.Enable();
            }
            else
            {
                transferAction.Disable();
                movementAction.Disable();
                landAction.Disable();
                lzWindowAction.Disable();
            }
        }
        #endregion

        #region//Input methods
        //Landing
        void OnLandInput(InputAction.CallbackContext context)
        {
            landInput = true;
        }
        public void ExpendLandInput() { landInput = false; }

        //Transfer
        void OnTransferInput(InputAction.CallbackContext context)
        {
            if(currentBuffer < transferFrameBuffer) return;
            transferInput = true;
        }
        public void ExpendTransferInput() { transferInput = false; }

        public void IncreaseBuffer()
        {
            currentBuffer = Mathf.Min(currentBuffer + 1, transferFrameBuffer);
        }

        //LZ Window
        void OnLZWindowInput(InputAction.CallbackContext context)
        {
            lzWindowInput = true;
        }
        void ExpendLZWindowInput(InputAction.CallbackContext context) { lzWindowInput = false; }
        #endregion
    }
}