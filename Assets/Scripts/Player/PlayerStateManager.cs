using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    //11:42 https://www.youtube.com/watch?v=Vt8aZDPzRjI
    void Awake()
    {
        currentState = IdleState;
        currentState.RunOnce(this);
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
        FindPlayerObject();

        // Counter countdowns
        currentState.jumpBufferCounter -= 1;
        currentState.coyoteTimeCounter -= 1;

        // Set jump buffer if pressed
        if(Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp))
        {
            Debug.Log("Jump");
            currentState.jumpBufferCounter = 5;
        }
        // Toggle sprint
        currentState.fakeSprintToggle = true;
        if (Input.GetKeyDown(SettingsData.Instance._InputSprint) && currentState.fakeSprintToggle)
        {
            Debug.Log("Toggle sprint " + currentState.fakeSprintToggle);
            currentState.sprinting = !currentState.sprinting;
        }
        // No toggle sprint
        if (currentState.fakeSprintToggle == false && Input.GetKeyDown(SettingsData.Instance._InputSprint))
        {
            Debug.Log("Holding Sprint " + currentState.fakeSprintToggle);
            currentState.sprinting = true;
        }
        else if (currentState.fakeSprintToggle == false && Input.GetKeyUp(SettingsData.Instance._InputSprint))
        {
            Debug.Log("Let go of sprint " + currentState.fakeSprintToggle);
            currentState.sprinting = false;
        }
    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    public void FindPlayerObject()
    {
        //currentState.PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }
}