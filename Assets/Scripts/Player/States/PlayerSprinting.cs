using UnityEngine;

public class PlayerSprinting : PlayerAbstract
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Sprinting / Sprinting State");
        //Switch back to idle after code is done running
    }
    public override void UpdateState(PlayerStateManager player)
    {
        
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
