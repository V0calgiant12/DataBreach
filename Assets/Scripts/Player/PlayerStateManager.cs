using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    public PlayerWallCling WallClingState = new PlayerWallCling();
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

        // Counter countdowns
        currentState.jumpBufferCounter -= 1;
        currentState.coyoteTimeCounter -= 1;

        // Set jump buffer if pressed
        if(Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp))
        {
            currentState.jumpBufferCounter = 5;
        }
    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
}