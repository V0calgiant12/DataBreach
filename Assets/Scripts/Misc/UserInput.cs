using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance;
    [Header("References")]
    private PlayerInput _playerInput;
    private InputAction _moveAction;
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
    public bool KeyDownJump {get; private set;}
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
    }
    private void UpdateInputs()
    {
        // Game Controls
        MovementInput = _moveAction.ReadValue<Vector2>();
        KeyDownJump = _jumpAction.WasPressedThisFrame();
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
}
