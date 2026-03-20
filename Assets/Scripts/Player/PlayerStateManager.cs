using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    public GroundedUpdater GroundedUpdater;
    //11:42 https://www.youtube.com/watch?v=Vt8aZDPzRjI
    void Start()
    {
        currentState = IdleState;
        currentState.RunOnce(this);
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
        GroundedUpdater.GroundedUpdate();
        
    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
