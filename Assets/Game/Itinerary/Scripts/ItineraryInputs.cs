// GENERATED AUTOMATICALLY FROM 'Assets/Game/Itinerary/Scripts/ItineraryInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ItineraryInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ItineraryInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ItineraryInputs"",
    ""maps"": [
        {
            ""name"": ""Itinerary"",
            ""id"": ""8b62fd70-7d08-489c-a662-840dc7070d79"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""e0d62cf9-c03a-4308-830e-c416155f63a2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""05e84595-5778-4dbd-aad6-ddc5edb97d7a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""eef9178c-f556-41b1-9e36-591fbd94181a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""AD"",
                    ""id"": ""8ca8578d-7500-48b2-abdb-ab3e3a4e6fb0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a4800fd5-f0e7-436a-a93e-07049ffdf850"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""379a56ac-9e66-495a-9304-41494f6d9ed1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""9a8008d0-8fd7-4975-9d4d-6666a80f95c2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""a1b55308-5538-4929-bd3b-eef6b270ae67"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6f6fe79a-f143-4cff-bf66-293f1de6ab6c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""12e0c63e-e243-41b8-8084-f20be9ab3ca6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c703a88b-ed61-48d9-97b6-1ac4f97e13e2"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a9af64a8-bc7a-4995-9749-053365ff7ae8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right Stick"",
                    ""id"": ""8ec97cdd-bd25-426d-a9de-8116502f4919"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""de6bd362-9c7c-4b33-bec2-ed2d0ed39f55"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fcf8fc9f-2f14-4738-babb-29c11829b00c"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""98fb0050-153a-4656-a5ce-f9d34594b536"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""65f50f2a-7ab9-4f3f-9781-0be44e3cf965"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e9a97717-424a-4ba6-afa2-d5b31523df30"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""aa235fa4-d029-4574-933b-5ef924923637"",
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
                    ""id"": ""6f9eaf14-21f0-44f9-898a-e059a39a7a55"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""408096a9-c1e1-4a42-8709-cadbb5a0bfbb"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0b5e017f-33f7-4164-9812-d95e5c401073"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Itinerary
        m_Itinerary = asset.FindActionMap("Itinerary", throwIfNotFound: true);
        m_Itinerary_Move = m_Itinerary.FindAction("Move", throwIfNotFound: true);
        m_Itinerary_Select = m_Itinerary.FindAction("Select", throwIfNotFound: true);
        m_Itinerary_Toggle = m_Itinerary.FindAction("Toggle", throwIfNotFound: true);
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

    // Itinerary
    private readonly InputActionMap m_Itinerary;
    private IItineraryActions m_ItineraryActionsCallbackInterface;
    private readonly InputAction m_Itinerary_Move;
    private readonly InputAction m_Itinerary_Select;
    private readonly InputAction m_Itinerary_Toggle;
    public struct ItineraryActions
    {
        private @ItineraryInputs m_Wrapper;
        public ItineraryActions(@ItineraryInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Itinerary_Move;
        public InputAction @Select => m_Wrapper.m_Itinerary_Select;
        public InputAction @Toggle => m_Wrapper.m_Itinerary_Toggle;
        public InputActionMap Get() { return m_Wrapper.m_Itinerary; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ItineraryActions set) { return set.Get(); }
        public void SetCallbacks(IItineraryActions instance)
        {
            if (m_Wrapper.m_ItineraryActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnMove;
                @Select.started -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnSelect;
                @Toggle.started -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnToggle;
                @Toggle.performed -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnToggle;
                @Toggle.canceled -= m_Wrapper.m_ItineraryActionsCallbackInterface.OnToggle;
            }
            m_Wrapper.m_ItineraryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Toggle.started += instance.OnToggle;
                @Toggle.performed += instance.OnToggle;
                @Toggle.canceled += instance.OnToggle;
            }
        }
    }
    public ItineraryActions @Itinerary => new ItineraryActions(this);
    public interface IItineraryActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnToggle(InputAction.CallbackContext context);
    }
}
