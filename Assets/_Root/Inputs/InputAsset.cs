//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/_Root/Inputs/InputAsset.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputAsset: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputAsset()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputAsset"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""df70fa95-8a34-4494-b137-73ab6b9c7d37"",
            ""actions"": [
                {
                    ""name"": ""MoveBody"",
                    ""type"": ""Value"",
                    ""id"": ""351f2ccd-1f9f-44bf-9bec-d62ac5c5f408"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateBody"",
                    ""type"": ""Value"",
                    ""id"": ""3e2406c0-00ea-4b95-87a2-07fad85352ec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GripLeft"",
                    ""type"": ""Value"",
                    ""id"": ""50d28564-561d-4d17-8d62-ca6ace79f7d2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GripRight"",
                    ""type"": ""Value"",
                    ""id"": ""18f91b56-b099-4de3-9f75-b7d0b548f331"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""GripButtonLeft"",
                    ""type"": ""Button"",
                    ""id"": ""1da2f718-9120-4912-baca-bf853040236c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GripButtonRight"",
                    ""type"": ""Button"",
                    ""id"": ""6c99d658-c5c8-4379-bcdf-c6bd9a5a3460"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveMouse"",
                    ""type"": ""Value"",
                    ""id"": ""5f1edc2e-6299-4acf-aac0-4301e1fb991f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateHead"",
                    ""type"": ""Value"",
                    ""id"": ""bb6b6db1-45c9-4d08-bc44-aa21d8ec0980"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""TriggerButtonRight"",
                    ""type"": ""Value"",
                    ""id"": ""0e7372d7-31e0-4ebb-b08d-f08858f62b73"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""00ca640b-d935-4593-8157-c05846ea39b3"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBody"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e2062cb9-1b15-46a2-838c-2f8d72a0bdd9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveBody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""320bffee-a40b-4347-ac70-c210eb8bc73a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MoveBody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""eef9e8b9-c28a-42e0-b785-f4e26d85f54e"",
                    ""path"": ""<XRController>{LeftHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc93305b-21f4-4825-95fd-233a81e8b708"",
                    ""path"": ""<XRController>{LeftHand}/grip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65db6ea3-7022-474c-91b4-98ebb4461190"",
                    ""path"": ""<XRController>{RightHand}/grip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8169a6d0-b660-437f-8468-43d528189ba8"",
                    ""path"": ""<XRController>{LeftHand}/gripButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripButtonLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5b26a91-3049-400e-8296-a21a07b3a044"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripButtonLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ebb20e1-0d89-48b3-b232-8135da42d732"",
                    ""path"": ""<XRController>{RightHand}/gripButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripButtonRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47240bbd-dbc3-4d31-a142-cb21a25387a7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripButtonRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e06b3e72-d084-4ec4-95ad-4022e312e5e9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0daba71-4e2f-476e-8276-dc9501d383e9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GripRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""05fa8cf7-16a3-44d7-beb5-68d7aa353576"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateBody"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ec0f8e9d-7995-4359-a589-8bc1b9e2750c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RotateBody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""baa3b1c0-51a0-41e1-8694-fec558363743"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RotateBody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fbcb88cc-6b40-4021-869f-e62e55681967"",
                    ""path"": ""<XRController>{RightHand}/joystick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateBody"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""be1f79ce-4803-4a2e-b078-85bce54efd9f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0a541304-ebbe-4889-be81-0c1e7a9cab88"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7f36e37e-ab5e-400c-96e5-e7a9ef22e26d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1a034428-e88a-4516-999c-296469ce1fc4"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateHead"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""caa33247-149b-45cd-b801-f1947ee8704d"",
                    ""path"": ""<XRController>/triggerButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerButtonRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8df9723-3f9d-41cb-90ec-991dcd15d97c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerButtonRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_MoveBody = m_Player.FindAction("MoveBody", throwIfNotFound: true);
        m_Player_RotateBody = m_Player.FindAction("RotateBody", throwIfNotFound: true);
        m_Player_GripLeft = m_Player.FindAction("GripLeft", throwIfNotFound: true);
        m_Player_GripRight = m_Player.FindAction("GripRight", throwIfNotFound: true);
        m_Player_GripButtonLeft = m_Player.FindAction("GripButtonLeft", throwIfNotFound: true);
        m_Player_GripButtonRight = m_Player.FindAction("GripButtonRight", throwIfNotFound: true);
        m_Player_MoveMouse = m_Player.FindAction("MoveMouse", throwIfNotFound: true);
        m_Player_RotateHead = m_Player.FindAction("RotateHead", throwIfNotFound: true);
        m_Player_TriggerButtonRight = m_Player.FindAction("TriggerButtonRight", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_MoveBody;
    private readonly InputAction m_Player_RotateBody;
    private readonly InputAction m_Player_GripLeft;
    private readonly InputAction m_Player_GripRight;
    private readonly InputAction m_Player_GripButtonLeft;
    private readonly InputAction m_Player_GripButtonRight;
    private readonly InputAction m_Player_MoveMouse;
    private readonly InputAction m_Player_RotateHead;
    private readonly InputAction m_Player_TriggerButtonRight;
    public struct PlayerActions
    {
        private @InputAsset m_Wrapper;
        public PlayerActions(@InputAsset wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveBody => m_Wrapper.m_Player_MoveBody;
        public InputAction @RotateBody => m_Wrapper.m_Player_RotateBody;
        public InputAction @GripLeft => m_Wrapper.m_Player_GripLeft;
        public InputAction @GripRight => m_Wrapper.m_Player_GripRight;
        public InputAction @GripButtonLeft => m_Wrapper.m_Player_GripButtonLeft;
        public InputAction @GripButtonRight => m_Wrapper.m_Player_GripButtonRight;
        public InputAction @MoveMouse => m_Wrapper.m_Player_MoveMouse;
        public InputAction @RotateHead => m_Wrapper.m_Player_RotateHead;
        public InputAction @TriggerButtonRight => m_Wrapper.m_Player_TriggerButtonRight;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @MoveBody.started += instance.OnMoveBody;
            @MoveBody.performed += instance.OnMoveBody;
            @MoveBody.canceled += instance.OnMoveBody;
            @RotateBody.started += instance.OnRotateBody;
            @RotateBody.performed += instance.OnRotateBody;
            @RotateBody.canceled += instance.OnRotateBody;
            @GripLeft.started += instance.OnGripLeft;
            @GripLeft.performed += instance.OnGripLeft;
            @GripLeft.canceled += instance.OnGripLeft;
            @GripRight.started += instance.OnGripRight;
            @GripRight.performed += instance.OnGripRight;
            @GripRight.canceled += instance.OnGripRight;
            @GripButtonLeft.started += instance.OnGripButtonLeft;
            @GripButtonLeft.performed += instance.OnGripButtonLeft;
            @GripButtonLeft.canceled += instance.OnGripButtonLeft;
            @GripButtonRight.started += instance.OnGripButtonRight;
            @GripButtonRight.performed += instance.OnGripButtonRight;
            @GripButtonRight.canceled += instance.OnGripButtonRight;
            @MoveMouse.started += instance.OnMoveMouse;
            @MoveMouse.performed += instance.OnMoveMouse;
            @MoveMouse.canceled += instance.OnMoveMouse;
            @RotateHead.started += instance.OnRotateHead;
            @RotateHead.performed += instance.OnRotateHead;
            @RotateHead.canceled += instance.OnRotateHead;
            @TriggerButtonRight.started += instance.OnTriggerButtonRight;
            @TriggerButtonRight.performed += instance.OnTriggerButtonRight;
            @TriggerButtonRight.canceled += instance.OnTriggerButtonRight;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @MoveBody.started -= instance.OnMoveBody;
            @MoveBody.performed -= instance.OnMoveBody;
            @MoveBody.canceled -= instance.OnMoveBody;
            @RotateBody.started -= instance.OnRotateBody;
            @RotateBody.performed -= instance.OnRotateBody;
            @RotateBody.canceled -= instance.OnRotateBody;
            @GripLeft.started -= instance.OnGripLeft;
            @GripLeft.performed -= instance.OnGripLeft;
            @GripLeft.canceled -= instance.OnGripLeft;
            @GripRight.started -= instance.OnGripRight;
            @GripRight.performed -= instance.OnGripRight;
            @GripRight.canceled -= instance.OnGripRight;
            @GripButtonLeft.started -= instance.OnGripButtonLeft;
            @GripButtonLeft.performed -= instance.OnGripButtonLeft;
            @GripButtonLeft.canceled -= instance.OnGripButtonLeft;
            @GripButtonRight.started -= instance.OnGripButtonRight;
            @GripButtonRight.performed -= instance.OnGripButtonRight;
            @GripButtonRight.canceled -= instance.OnGripButtonRight;
            @MoveMouse.started -= instance.OnMoveMouse;
            @MoveMouse.performed -= instance.OnMoveMouse;
            @MoveMouse.canceled -= instance.OnMoveMouse;
            @RotateHead.started -= instance.OnRotateHead;
            @RotateHead.performed -= instance.OnRotateHead;
            @RotateHead.canceled -= instance.OnRotateHead;
            @TriggerButtonRight.started -= instance.OnTriggerButtonRight;
            @TriggerButtonRight.performed -= instance.OnTriggerButtonRight;
            @TriggerButtonRight.canceled -= instance.OnTriggerButtonRight;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMoveBody(InputAction.CallbackContext context);
        void OnRotateBody(InputAction.CallbackContext context);
        void OnGripLeft(InputAction.CallbackContext context);
        void OnGripRight(InputAction.CallbackContext context);
        void OnGripButtonLeft(InputAction.CallbackContext context);
        void OnGripButtonRight(InputAction.CallbackContext context);
        void OnMoveMouse(InputAction.CallbackContext context);
        void OnRotateHead(InputAction.CallbackContext context);
        void OnTriggerButtonRight(InputAction.CallbackContext context);
    }
}
