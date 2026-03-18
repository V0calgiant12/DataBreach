using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerAbstract currentState;
    PlayerAir AirState = new PlayerAir();
    PlayerCrouching CrouchingState = new PlayerCrouching();
    PlayerIdle IdleState = new PlayerIdle();
    PlayerSprinting SprintingState = new PlayerSprinting();
    PlayerWalking WalkingState = new PlayerWalking();
    //11:42 https://www.youtube.com/watch?v=Vt8aZDPzRjI
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
        Debug.Log("Player Idle / Idle State");
    }
    void Update()
    {
        
    }
    void SwitchState(PlayerAbstract state)
    {
        
    }
}
