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
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
