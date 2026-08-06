using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    private InputActionAsset InputActions;
    [Header("References")]
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _upAction;
    private InputAction _crouchAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _sprintAction;
    private InputAction _interactAction;
    private InputAction _menuAction;
    private InputAction _navigateAction;
    private InputAction _submitAction;
    private InputAction _cancelAction;
    private InputAction _rightMenuAction;
    private InputAction _leftMenuAction;

    [Header("Game Controls")]
    public Vector2 MovementInput {get; private set;}
    public bool KeyDownUpInput {get; private set;}
    public bool KeyUpMovement {get; private set;}
    public bool KeyDownCrouch {get; private set;}
    public bool KeyDownJump {get; private set;}
    public bool KeyHeldDownJump {get; private set;}
    public bool KeyDownAttack {get; private set;}
    public bool KeyDownSprint {get; private set;}
    public bool KeyHeldDownSprint {get; private set;}
    public bool KeyDownInteract {get; private set;}
    [Header("Menu Controls")]
    public bool MenuInput {get; private set;}
    public Vector2 NavigateInput {get; private set;}
    public bool NavigateDown {get; private set;}
    public bool SubmitInput {get; private set;}
    public bool CancelInput {get; private set;}
    public bool RightMenuInput {get; private set;}
    public bool LeftMenuInput {get; private set;}
    [Header("Other")]
    public ControllerSchemes currentController;
    public enum ControllerSchemes
    {
        KBM,
        Controller
    }
    public ControllerTypes controllerType;
    public enum ControllerTypes
    {
        KBM,
        Switch,
        Xbox,
        PS,
        Other
    }
    public string joystickName;
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        _playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }
    private void SetupInputActions()
    {
        // Game Controls
        _moveAction = _playerInput.actions["Move"];
        _upAction = _playerInput.actions["UpInput"];
        _crouchAction = _playerInput.actions["Crouch"];
        _jumpAction = _playerInput.actions["Jump"];
        _attackAction = _playerInput.actions["Attack"];
        _sprintAction = _playerInput.actions["Sprint"];
        _interactAction = _playerInput.actions["Interact"];

        // Menu Controls
        _menuAction = _playerInput.actions["Menu"];
        _navigateAction = _playerInput.actions["Navigate"];
        _submitAction = _playerInput.actions["Submit"];
        _cancelAction = _playerInput.actions["Cancel"];
        _rightMenuAction = _playerInput.actions["RightMenu"];
        _leftMenuAction = _playerInput.actions["LeftMenu"];

    }
    void Update()
    {
        UpdateInputs();
        switch (_playerInput.currentControlScheme)
        {
            case("Keyboard&Mouse"):
                currentController = ControllerSchemes.KBM;
                break;
            case("Gamepad"):
                currentController = ControllerSchemes.Controller;
                break;
        }
        if(Gamepad.current != null)
        {
            joystickName = Gamepad.current.displayName.ToString();
            if(Gamepad.current.displayName.ToLower().Contains("switch")||Gamepad.current.displayName.ToLower().Contains("wireless gamepad"))
            {
                controllerType = ControllerTypes.Switch;
                return;
            }
            else if(Gamepad.current.displayName.ToLower().Contains("xbox"))
            {
                controllerType = ControllerTypes.Xbox;
                return;
            }
            else if(Gamepad.current.displayName.ToLower().Contains("playstation"))
            {
                controllerType = ControllerTypes.PS;
                return;
            }
            }
        else
        {
            controllerType = ControllerTypes.KBM;
            return;
        }
    }
    private void UpdateInputs()
    {
        // Game Controls
        MovementInput = _moveAction.ReadValue<Vector2>();
        KeyDownUpInput = _upAction.WasPressedThisFrame();
        KeyUpMovement = _moveAction.WasReleasedThisFrame();
        KeyDownCrouch = _crouchAction.WasPressedThisFrame();
        KeyDownJump = _jumpAction.WasPressedThisFrame();
        KeyHeldDownJump = _jumpAction.IsPressed();
        KeyDownAttack = _attackAction.WasPressedThisFrame();
        KeyHeldDownSprint = _sprintAction.IsPressed();
        KeyDownSprint = _sprintAction.WasPressedThisFrame();
        KeyDownInteract = _interactAction.WasPressedThisFrame();

        // Menu Controls
        MenuInput = _menuAction.WasPressedThisFrame();
        NavigateInput = _navigateAction.ReadValue<Vector2>();
        NavigateDown = _navigateAction.WasPressedThisFrame();
        SubmitInput = _submitAction.WasPressedThisFrame();
        CancelInput = _cancelAction.WasPressedThisFrame();
        RightMenuInput = _rightMenuAction.WasPressedThisFrame();
        LeftMenuInput = _leftMenuAction.WasPressedThisFrame();
    }
    public void UpdateKeyBinds()
    {
        _playerInput.actions.Disable();
        _jumpAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= "<Keyboard>/"+(""+ SettingsData.Instance._InputJump).ToLowerInvariant()});
        _attackAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= "<Keyboard>/"+(""+ SettingsData.Instance._InputAttack).ToLowerInvariant()});
        _sprintAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= "<Keyboard>/"+(""+ SettingsData.Instance._InputSprint).ToLowerInvariant()});
        _interactAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= "<Keyboard>/"+(""+ SettingsData.Instance._InputInteract).ToLowerInvariant()});
        _upAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= "<Keyboard>/"+(""+ SettingsData.Instance._InputUp).ToLowerInvariant()});
        _crouchAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= "<Keyboard>/"+(""+ SettingsData.Instance._InputDown).ToLowerInvariant()});
        _moveAction.ApplyBindingOverride(new InputBinding{groups = "Keyboard&Mouse",overridePath= ""});
        _moveAction.AddCompositeBinding("2DVector")
            .With("Up","<Keyboard>/"+(""+ SettingsData.Instance._InputUp).ToLowerInvariant())
            .With("Down","<Keyboard>/"+(""+ SettingsData.Instance._InputDown).ToLowerInvariant())
            .With("Left","<Keyboard>/"+(""+ SettingsData.Instance._InputLeft).ToLowerInvariant())
            .With("Right","<Keyboard>/"+(""+ SettingsData.Instance._InputRight).ToLowerInvariant());
        AddGamepadControls();
    }
    private void AddGamepadControls()
    {
        _jumpAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/ButtonWest"});
        _attackAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/ButtonEast"});
        _sprintAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/ButtonSouth"});
        _interactAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/ButtonNorth"});
        _upAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/LeftStick/Up"});
        _upAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/DPad/Up"});
        _crouchAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/LeftStick/Down"});
        _crouchAction.AddBinding(new InputBinding{groups = "Gamepad", overridePath="<Gamepad>/DPad/Down"});
        _playerInput.actions.Enable();
    }
}
