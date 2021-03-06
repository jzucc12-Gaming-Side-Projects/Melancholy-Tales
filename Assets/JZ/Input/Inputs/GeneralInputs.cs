// GENERATED AUTOMATICALLY FROM 'Assets/JZ/Input/Inputs/GeneralInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GeneralInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GeneralInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GeneralInputs"",
    ""maps"": [
        {
            ""name"": ""Menus"",
            ""id"": ""58a59716-1d0a-4202-b243-cbdf94058412"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Button"",
                    ""id"": ""2ed9a746-356e-4097-9dbb-d4eda4e627de"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""fbe7ec4a-fa0d-4cb1-aa45-1494133b8553"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""b9095a05-c4a2-4eb4-a6b0-51fd8ef2ac33"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""2ef6ea57-9862-467d-a206-2df184283346"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1d892405-f784-41ba-89f1-5975dadf4405"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d58e0eed-24df-418a-89e9-a0f3ce76778e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""144165e9-fdbb-429a-93f9-31ec83239cc7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6f11deba-5187-4293-9645-04f1e3fe9d45"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Dpad"",
                    ""id"": ""2303438a-01f1-4006-9cae-ff905c09d1b4"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5a3623f1-f61a-432f-8f98-98fad66c9ba3"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""897af3ae-2cb8-4fbd-9434-7dfa22e627ef"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4411ec65-a826-4dc7-9926-74a1f59138b8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ada9acc2-79cc-4142-89b9-af754229116a"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""7c8b4c53-273a-4d0d-85a4-f09435ce0f24"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e516d2fd-36b2-4044-bcdb-53e24b86928c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eb9cd374-561b-4020-8e9c-250c36da55d4"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8e503c3f-81c6-4422-80fe-9bf852217a99"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aa357e3f-d582-4e21-b5e7-12409f094e96"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""27a94c08-7f4c-42b8-ae39-3b459bd09385"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7f7c5aa6-c509-4bc1-abb4-2c9f653e2a7b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""990dd678-73fd-465d-8de3-a420c152ac93"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""dd313ea1-7ef5-4cf7-b278-6409c4060639"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""67a92dcc-e1aa-4ac9-8fb5-d1693f6ee261"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d23ecff2-b0ff-44ea-9f87-09863241a01a"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50dc0b0c-1e6c-4920-8e53-f58dbbd35b05"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Triggers"",
                    ""id"": ""4d7d2b5f-5231-4549-b80d-97bd983d847b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5b0d764e-a30f-438b-92ce-22706c1af8f8"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3fe92740-8536-4a87-8bc6-edee6de3315a"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""QE"",
                    ""id"": ""eea92126-167d-4a25-9c78-6ec056737ad1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f9c96a89-4332-4aeb-8a54-5eb8746a07f6"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c7ffd0e6-4f09-4107-8ed6-8dc11a7d7031"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Options"",
            ""id"": ""8bcc81e0-d099-4a50-ac06-218fecb97ff7"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""a92a603c-c6a8-456c-a5cc-b9b313e7fcfc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Fullscreen"",
                    ""type"": ""Button"",
                    ""id"": ""972459c7-7b40-43e6-b0ce-d7f27e4c9dc2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3a59070a-348b-421c-84ba-142e4a243e00"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66438b76-c353-4548-91bf-9af4d865af6f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd802eeb-73b6-48b6-9715-486297ae6559"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Fullscreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""babaa72e-8ec6-4952-89c6-dbfd2702f94f"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Fullscreen"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""2a7b6142-ada6-4735-b333-42add8270868"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Fullscreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""8bcdee01-22f8-46cb-9129-e66b0c8defbb"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Fullscreen"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_Navigate = m_Menus.FindAction("Navigate", throwIfNotFound: true);
        m_Menus_Select = m_Menus.FindAction("Select", throwIfNotFound: true);
        m_Menus_Skip = m_Menus.FindAction("Skip", throwIfNotFound: true);
        // Options
        m_Options = asset.FindActionMap("Options", throwIfNotFound: true);
        m_Options_Pause = m_Options.FindAction("Pause", throwIfNotFound: true);
        m_Options_ToggleFullscreen = m_Options.FindAction("Toggle Fullscreen", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_Navigate;
    private readonly InputAction m_Menus_Select;
    private readonly InputAction m_Menus_Skip;
    public struct MenusActions
    {
        private @GeneralInputs m_Wrapper;
        public MenusActions(@GeneralInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_Menus_Navigate;
        public InputAction @Select => m_Wrapper.m_Menus_Select;
        public InputAction @Skip => m_Wrapper.m_Menus_Skip;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnNavigate;
                @Select.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSelect;
                @Skip.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);

    // Options
    private readonly InputActionMap m_Options;
    private IOptionsActions m_OptionsActionsCallbackInterface;
    private readonly InputAction m_Options_Pause;
    private readonly InputAction m_Options_ToggleFullscreen;
    public struct OptionsActions
    {
        private @GeneralInputs m_Wrapper;
        public OptionsActions(@GeneralInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Options_Pause;
        public InputAction @ToggleFullscreen => m_Wrapper.m_Options_ToggleFullscreen;
        public InputActionMap Get() { return m_Wrapper.m_Options; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OptionsActions set) { return set.Get(); }
        public void SetCallbacks(IOptionsActions instance)
        {
            if (m_Wrapper.m_OptionsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_OptionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_OptionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_OptionsActionsCallbackInterface.OnPause;
                @ToggleFullscreen.started -= m_Wrapper.m_OptionsActionsCallbackInterface.OnToggleFullscreen;
                @ToggleFullscreen.performed -= m_Wrapper.m_OptionsActionsCallbackInterface.OnToggleFullscreen;
                @ToggleFullscreen.canceled -= m_Wrapper.m_OptionsActionsCallbackInterface.OnToggleFullscreen;
            }
            m_Wrapper.m_OptionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @ToggleFullscreen.started += instance.OnToggleFullscreen;
                @ToggleFullscreen.performed += instance.OnToggleFullscreen;
                @ToggleFullscreen.canceled += instance.OnToggleFullscreen;
            }
        }
    }
    public OptionsActions @Options => new OptionsActions(this);
    public interface IMenusActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
    }
    public interface IOptionsActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnToggleFullscreen(InputAction.CallbackContext context);
    }
}
