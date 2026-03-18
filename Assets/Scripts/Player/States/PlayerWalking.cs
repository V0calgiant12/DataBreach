using UnityEngine;

public class PlayerWalking : PlayerAbstract
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Walking / Walking State");
        //Switch back to idle after code is done running
    }
    public override void UpdateState(PlayerStateManager player)
    {
        
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
