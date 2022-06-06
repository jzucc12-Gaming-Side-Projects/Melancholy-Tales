using UnityEngine;
using UnityEngine.InputSystem;

namespace JZ.INPUT
{
    public class MenuingInputSystem : MyInputSystem
    {
        #region//Input actions
        InputAction menuNavigate;
        InputAction menuSelect;
        InputAction menuSkip;
        #endregion

        #region//Inputs
        public int xNav { get; private set; }
        public int yNav { get; private set; }
        public bool selected { get; private set; }
        public int skip { get; private set; }
        #endregion


        #region//Constructors
        public MenuingInputSystem(GeneralInputs _inputs) : base(_inputs)
        {
            var map = _inputs.Menus;
            menuNavigate = map.Navigate;
            menuSelect = map.Select;
            menuSkip = map.Skip;
        }

        public MenuingInputSystem(ShipInputs _inputs, bool land) : base(_inputs)
        {
            if(land)
            {
                var map = _inputs.LandMenus;
                menuNavigate = map.Navigate;
                menuSelect = map.Select;
            }
            else
            {
                var map = _inputs.Menus;
                menuNavigate = map.Navigate;
                menuSelect = map.Select;
            }
        }
        #endregion

        #region//Startup shutdown
        public override void InitializeInputs()
        {
            xNav = 0;
            yNav = 0;
            selected = false;
        }

        protected override void SubscribeEvents(bool _startUp)
        {
            if (_startUp)
            {
                menuNavigate.started += OnMenuNavigate;
                menuSelect.started += OnMenuSelect;
                menuNavigate.canceled += OnMenuNavigate;
                if(menuSkip != null) menuSkip.started += OnMenuSkip;
            }
            else
            {
                menuNavigate.started -= OnMenuNavigate;
                menuNavigate.canceled -= OnMenuNavigate;
                menuSelect.started -= OnMenuSelect;
                if(menuSkip != null) menuSkip.started -= OnMenuSkip;
            }
        }

        protected override void EnableActions(bool _startUp)
        {
            if (_startUp)
            {
                menuNavigate.Enable();
                menuSelect.Enable();
                if(menuSkip != null) menuSkip.Enable();
            }
            else
            {
                menuNavigate.Disable();
                menuSelect.Disable();
                if(menuSkip != null) menuSkip.Disable();
            }
        }
        #endregion

        #region//Menuing
        void OnMenuNavigate(InputAction.CallbackContext context)
        {
            xNav = Mathf.RoundToInt(menuNavigate.ReadValue<Vector2>().x);
            yNav = Mathf.RoundToInt(menuNavigate.ReadValue<Vector2>().y);
        }
        void OnMenuSelect(InputAction.CallbackContext context)
        {
            selected = true;
        }
        void OnMenuSkip(InputAction.CallbackContext context)
        {
            float value = menuSkip.ReadValue<float>();
            skip = (int)Mathf.Sign(value);
        }
        public void ExpendMenuSelect() { selected = false; }
        public void ExpendXDir() { xNav = 0; }
        public void ExpendYDir() { yNav = 0; }
        public void ExpendAllDir()
        {
            ExpendXDir();
            ExpendYDir();
        }
        public void ExpendSkip()
        {
            skip = 0;
        }
        #endregion
    }
}