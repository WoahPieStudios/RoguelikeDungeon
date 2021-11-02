// GENERATED AUTOMATICALLY FROM 'Assets/InputActions/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Navigation"",
            ""id"": ""18e78a2b-c1b9-4ce4-9baa-e787306fe78f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""e1f49753-1019-4a27-a56b-e08c1442844e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap Assist"",
                    ""type"": ""Button"",
                    ""id"": ""39d53d7b-79b8-42a7-965b-513a9052d080"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swap Off-Field"",
                    ""type"": ""Button"",
                    ""id"": ""ddaa23a9-7b43-4cc4-b1f0-52cbbdb34b2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""7369f48a-81e5-4a58-a6d6-fb83a8665651"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill"",
                    ""type"": ""Button"",
                    ""id"": ""3c49c4cb-f151-4411-8a31-8d1d19eb5d9d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ultimate"",
                    ""type"": ""Button"",
                    ""id"": ""9dd8f99c-c611-4b30-8d0c-61151d0f024e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ultimate Assist"",
                    ""type"": ""Button"",
                    ""id"": ""d2334f3e-0ef7-4367-b338-2855547b0a38"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f0c41196-36d9-48de-a9c6-489a0d9471bf"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""6b2cd54e-1323-4b07-8720-eafc5191a2ad"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""20364bb6-4d2f-4c93-98cd-cc2272de3b1d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""08d403d4-c51e-4606-a4fc-7a87b535ab9c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5bcf22b0-1240-41c1-8d4d-e0337dacc52d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""152d5dba-ebe7-4778-b9a6-55dfb9302cb5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""162d17fc-6c36-4388-8414-9e5c8f3d5576"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap Assist"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98acd980-757b-44a4-8bed-cf657affbcfc"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap Assist"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f8891fc-5c05-41d9-80c7-d813e4de1023"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap Off-Field"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07af541b-b651-45dc-baf3-379820e21c10"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Swap Off-Field"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ef9f24d-4fd4-46d3-9946-ef159e7bf6a7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""362453bc-c4f8-484a-9892-563bcc107965"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f89ec8a5-f073-4486-a4d2-a788dab29840"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d4df272b-dc30-4511-9e76-5895daeb6f04"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f93b772-e324-451f-b0c2-0c3bed2616bb"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""872d773e-13e9-44a1-9c96-2917b18ff935"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""920e1e96-a1bf-48ff-a1e3-053cb81100a3"",
                    ""path"": ""<Mouse>/forwardButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate Assist"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b8a5d94f-cdc9-4d54-98a7-fa810d6bc1cc"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ultimate Assist"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Navigation
        m_Navigation = asset.FindActionMap("Navigation", throwIfNotFound: true);
        m_Navigation_Movement = m_Navigation.FindAction("Movement", throwIfNotFound: true);
        m_Navigation_SwapAssist = m_Navigation.FindAction("Swap Assist", throwIfNotFound: true);
        m_Navigation_SwapOffField = m_Navigation.FindAction("Swap Off-Field", throwIfNotFound: true);
        m_Navigation_Attack = m_Navigation.FindAction("Attack", throwIfNotFound: true);
        m_Navigation_Skill = m_Navigation.FindAction("Skill", throwIfNotFound: true);
        m_Navigation_Ultimate = m_Navigation.FindAction("Ultimate", throwIfNotFound: true);
        m_Navigation_UltimateAssist = m_Navigation.FindAction("Ultimate Assist", throwIfNotFound: true);
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

    // Navigation
    private readonly InputActionMap m_Navigation;
    private INavigationActions m_NavigationActionsCallbackInterface;
    private readonly InputAction m_Navigation_Movement;
    private readonly InputAction m_Navigation_SwapAssist;
    private readonly InputAction m_Navigation_SwapOffField;
    private readonly InputAction m_Navigation_Attack;
    private readonly InputAction m_Navigation_Skill;
    private readonly InputAction m_Navigation_Ultimate;
    private readonly InputAction m_Navigation_UltimateAssist;
    public struct NavigationActions
    {
        private @PlayerControls m_Wrapper;
        public NavigationActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Navigation_Movement;
        public InputAction @SwapAssist => m_Wrapper.m_Navigation_SwapAssist;
        public InputAction @SwapOffField => m_Wrapper.m_Navigation_SwapOffField;
        public InputAction @Attack => m_Wrapper.m_Navigation_Attack;
        public InputAction @Skill => m_Wrapper.m_Navigation_Skill;
        public InputAction @Ultimate => m_Wrapper.m_Navigation_Ultimate;
        public InputAction @UltimateAssist => m_Wrapper.m_Navigation_UltimateAssist;
        public InputActionMap Get() { return m_Wrapper.m_Navigation; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NavigationActions set) { return set.Get(); }
        public void SetCallbacks(INavigationActions instance)
        {
            if (m_Wrapper.m_NavigationActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnMovement;
                @SwapAssist.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSwapAssist;
                @SwapAssist.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSwapAssist;
                @SwapAssist.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSwapAssist;
                @SwapOffField.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSwapOffField;
                @SwapOffField.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSwapOffField;
                @SwapOffField.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSwapOffField;
                @Attack.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnAttack;
                @Skill.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSkill;
                @Skill.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSkill;
                @Skill.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnSkill;
                @Ultimate.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnUltimate;
                @Ultimate.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnUltimate;
                @Ultimate.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnUltimate;
                @UltimateAssist.started -= m_Wrapper.m_NavigationActionsCallbackInterface.OnUltimateAssist;
                @UltimateAssist.performed -= m_Wrapper.m_NavigationActionsCallbackInterface.OnUltimateAssist;
                @UltimateAssist.canceled -= m_Wrapper.m_NavigationActionsCallbackInterface.OnUltimateAssist;
            }
            m_Wrapper.m_NavigationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @SwapAssist.started += instance.OnSwapAssist;
                @SwapAssist.performed += instance.OnSwapAssist;
                @SwapAssist.canceled += instance.OnSwapAssist;
                @SwapOffField.started += instance.OnSwapOffField;
                @SwapOffField.performed += instance.OnSwapOffField;
                @SwapOffField.canceled += instance.OnSwapOffField;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Skill.started += instance.OnSkill;
                @Skill.performed += instance.OnSkill;
                @Skill.canceled += instance.OnSkill;
                @Ultimate.started += instance.OnUltimate;
                @Ultimate.performed += instance.OnUltimate;
                @Ultimate.canceled += instance.OnUltimate;
                @UltimateAssist.started += instance.OnUltimateAssist;
                @UltimateAssist.performed += instance.OnUltimateAssist;
                @UltimateAssist.canceled += instance.OnUltimateAssist;
            }
        }
    }
    public NavigationActions @Navigation => new NavigationActions(this);
    public interface INavigationActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSwapAssist(InputAction.CallbackContext context);
        void OnSwapOffField(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnSkill(InputAction.CallbackContext context);
        void OnUltimate(InputAction.CallbackContext context);
        void OnUltimateAssist(InputAction.CallbackContext context);
    }
}
