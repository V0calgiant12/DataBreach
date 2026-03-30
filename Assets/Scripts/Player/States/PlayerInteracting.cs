using System.Collections;
using UnityEngine;

public class PlayerInteracting : PlayerAbstract
{
    private int frame = 0;
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        player.playerData.interacting = true;
        frame = 0;
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        player.playerData.PlayerRb.linearVelocity = new UnityEngine.Vector2(0,player.playerData.PlayerRb.linearVelocityY);
        if(frame > 10)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputInteract) && TextWrite.Instance._Writing == false)
            {
                player.playerData.interacting = false;
                TextWrite.Instance.Close();
                player.SwitchState(player.IdleState);
            }
        }
        else
        {
            frame += 1;
        }
    }
}
