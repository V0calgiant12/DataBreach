using UnityEngine;

public class PlayerAir : PlayerAbstract
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is in the air / Air State");
        //Switch back to idle after code is done running
    }
    public override void UpdateState(PlayerStateManager player)
    {
        
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
