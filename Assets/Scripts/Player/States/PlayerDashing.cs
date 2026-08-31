using UnityEngine;

public class PlayerDashing : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        player.playerData.resetVelocity = false;
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
}