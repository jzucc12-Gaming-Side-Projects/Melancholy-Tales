using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace  JZ.INPUT
{
    public class CutsceneInputSystem : MyInputSystem
    {
        #region //Input actions
        public InputAction skipSceneAction { get; private set; }
        public InputAction nextLineAction { get; private set; }
        #endregion

        #region //Inputs
        public float startHoldTime { get; private set; }
        public bool nextLineInput { get; private set; }
        #endregion


        #region //Constructors
        public CutsceneInputSystem(IInputActionCollection _inputs) : base(_inputs) 
        { 
            var cutsceneInput = (CutsceneInputs)_inputs;
            var map = cutsceneInput.Map;
            skipSceneAction = map.SkipScene;
            nextLineAction = map.NextLine;
        }
        #endregion

        #region //Set up
        public override void InitializeInputs()
        {
            startHoldTime = -1;
            nextLineInput = false;
        }

        protected override void EnableActions(bool _startUp)
        {
            if(_startUp)
            {
                skipSceneAction.Enable();
                nextLineAction.Enable();
            }
            else
            {
                skipSceneAction.Disable();
                nextLineAction.Disable();
            }
        }

        protected override void SubscribeEvents(bool _startUp)
        {
            if(_startUp)
            {
                skipSceneAction.started += StartSkipSceneHold;
                skipSceneAction.canceled += StopSkipSceneHold;
                nextLineAction.started += OnNextLineInput;
            }
            else
            {
                skipSceneAction.started -= StartSkipSceneHold;
                skipSceneAction.canceled -= StopSkipSceneHold;
                nextLineAction.started -= OnNextLineInput;
            }
        }
        #endregion

        #region //Skip Scene Inputs
        void StartSkipSceneHold(InputAction.CallbackContext _context)
        {
            startHoldTime = (float)_context.startTime;
        }

        void StopSkipSceneHold(InputAction.CallbackContext _context)
        {
            startHoldTime = -1;
        }

        public void StopSkipSceneHold()
        {
            startHoldTime = -1;
        }

        public bool GetIsHolding() => startHoldTime != -1;
        #endregion

        #region //Next Line Inputs
        void OnNextLineInput(InputAction.CallbackContext _context)
        {
            nextLineInput = true;
        }

        public void ExpendNextLineInput()
        {
            nextLineInput = false;
        }
        #endregion
    }
}