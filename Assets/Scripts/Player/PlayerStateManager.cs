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
    public override void EnterState(PlayerStateManager player) 
    {

    }
    public override void UpdateState(PlayerStateManager player)
    {

    }
    public override void OnCollisionEnter(PlayerStateManager player, Collision collision) 
    {
        
    }
}
