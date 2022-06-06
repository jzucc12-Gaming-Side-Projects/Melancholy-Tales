// using UnityEngine.InputSystem;
// using UnityEngine;
// using JZ.INPUT;

// namespace CFR.INPUT
// {
//     public class ManifestInputSystem : MyInputSystem
//     {
//         InputAction manifestAction;
//         public int manifestInput { get; private set; }
//         public bool manifestPressed => manifestInput != -1;
//         int maxHold;
//         bool isShip = false;


//         #region//Constructor
//         public ManifestInputSystem(IInputActionCollection _inputs, bool _isShip) : base(_inputs)
//         {
//             var manifestInputs = (ShipInputs)_inputs;
//             var map = manifestInputs.Player;
//             manifestAction = _isShip ? map.ShipManifest : map.LZManifest;
//             maxHold = _isShip ? Globals.maxShipHold : Globals.maxLZHold;
//             isShip = _isShip;
//         }
//         #endregion

//         #region//Startup shutdown
//         public override void InitializeInputs()
//         {
//             manifestInput = -1;
//         }

//         protected override void SubscribeEvents(bool _startUp)
//         {
//             if (_startUp)
//                 manifestAction.started += ManifestButton;
//             else
//                 manifestAction.started -= ManifestButton;
//         }

//         protected override void EnableActions(bool _startUp)
//         {
//             if (_startUp)
//                 manifestAction.Enable();
//             else
//                 manifestAction.Disable();
//         }
//         #endregion

//         #region//Ship Inputs
//         void ManifestButton(InputAction.CallbackContext context)
//         {
//             int button = manifestAction.controls.IndexOf(x => x == context.control);
//             int value = button;
//             //Debug.Log("before: " + value);

//             //Account for numpad
//             if (context.control.displayName.Contains("Num"))
//                 value -= maxHold;

//             //Account for if not on keyboard
//             if (GameSettings.isUsingGamepad)
//             {
//                 int hasNumpad = isShip ? 1 : 2;
//                 value -= (hasNumpad * maxHold + Gamepad.all.Count - 1);
//             }

//             //Debug.Log("after: " + value);
//             manifestInput = value;
//         }

//         public bool IsManifestInputPressed()
//         {
//             return manifestInput != -1;
//         }

//         public void ExpendManifestInput()
//         {
//             manifestInput = -1;
//         }

//         public int GetManifestInput()
//         {
//             return manifestInput;
//         }

//         public bool ValidManifestInput(int _max)
//         {
//             if (manifestInput >= _max)
//             {
//                 ExpendManifestInput();
//                 return false;
//             }

//             return true;
//         }
//         #endregion
//     }

// }