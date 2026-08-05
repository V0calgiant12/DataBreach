using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Controls")]
    public Vector2 MovementInput {get; private set;}
    public bool KeyDownJump {get; private set;}
    public bool KeyDownAttack {get; private set;}
    public bool KeyDownSprint {get; private set;}
    public bool KeyHeldDownSprint {get; private set;}
    public bool KeyDownInteract {get; private set;}
    public bool MenuInput {get; private set;}
    
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void SetupInputActions()
    {
        _moveAction = _playerInput.actions["Move"];
        _jumpAction = _playerInput.actions["Jump"];
        _attackAction = _playerInput.actions["Attack"];
        _sprintAction = _playerInput.actions["Sprint"];
        _interactAction = _playerInput.actions["Interact"];
        _menuAction = _playerInput.actions["Menu"];
    }
    void Update()
    {
        UpdateInputs();
    }
    private void UpdateInputs()
    {
        MovementInput = _moveAction.ReadValue<Vector2>();
        KeyDownJump = _jumpAction.WasPressedThisFrame();
        KeyDownAttack = _attackAction.WasPressedThisFrame();
        KeyHeldDownSprint = _sprintAction.IsPressed();
        KeyDownSprint = _sprintAction.WasPressedThisFrame();
        KeyDownInteract = _interactAction.WasPressedThisFrame();
        MenuInput = _menuAction.WasPressedThisFrame();
    }
}
