using UnityEngine.InputSystem;
using UnityEngine;

namespace JZ.INPUT
{
    public abstract class MyInputSystem
    {
        bool active = false;

        #region//Constructor
        public MyInputSystem(IInputActionCollection _inputs)
        {
            InitializeInputs();
        }

        public MyInputSystem(InputActionMap _map) { }
        #endregion

        #region//Startup shutdown
        public void Activate(bool _activate)
        {
            if (active == _activate) return;
            SubscribeEvents(_activate);
            EnableActions(_activate);
            active = _activate;
        }
        public abstract void InitializeInputs();
        protected abstract void SubscribeEvents(bool _startUp);
        protected abstract void EnableActions(bool _startUp);
        #endregion
    }
}