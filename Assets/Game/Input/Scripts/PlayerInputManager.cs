using UnityEngine.InputSystem;
using UnityEngine;
using JZ.INPUT;
using System;

namespace CFR.INPUT
{
    public class PlayerInputManager : MonoBehaviour
    {
        #region//Ship Input system
        ShipInputs shipInputs;
        PlayerInput playerInput;
        public ShipInputSystem shipSystem { get; private set; }
        public MenuingInputSystem menuSystem { get; private set; }
        public MenuingInputSystem landedMenuSystem { get; private set; }
        #endregion

        [SerializeField] bool isLocked = false;
        public event Func<bool> LandedCheck;


        #region//Monobehaviour
        private void Awake()
        {
            shipInputs = new ShipInputs();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            shipSystem = new ShipInputSystem(shipInputs);
            menuSystem = new MenuingInputSystem(shipInputs, false);
            landedMenuSystem = new MenuingInputSystem(shipInputs, true);
            StartUp();
            landedMenuSystem.Activate(false);
        }

        private void OnEnable()
        {
            StageInputManager.OnPause += LockInputs;
            if(shipSystem != null) StartUp();
        }

        private void OnDisable()
        {
            StageInputManager.OnPause -= LockInputs;
            ShutDown();
        }
        #endregion

        #region//Start up/shutdown
        void StartUp()
        {
            bool landingMenuOn = false;
            if(LandedCheck != null)
                landingMenuOn = (bool)LandedCheck.Invoke();

            landedMenuSystem.Activate(landingMenuOn);
            menuSystem.Activate(true);
            shipSystem.Activate(true);
        }

        void ShutDown()
        {
            landedMenuSystem.Activate(false);
            shipSystem.Activate(false);
            menuSystem.Activate(false);
        }
        #endregion

        #region//Other
        public void ReinitializeInputs()
        {
            landedMenuSystem.InitializeInputs();
            shipSystem.InitializeInputs();
            menuSystem.InitializeInputs();
        }
        #endregion

        #region//Events
        public void LockInputs(bool _lock)
        {
            if (_lock == isLocked) return;

            isLocked = _lock;
            if (_lock)
                ShutDown();
            else
                StartUp();
        }
        #endregion
    }
}