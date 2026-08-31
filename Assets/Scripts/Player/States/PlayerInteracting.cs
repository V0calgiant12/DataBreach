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
        player.playerData.interactingCooldown = 30;
        player.playerData.interacting = true;
        frame = 0;
        player.playerData.anim.SetBool("moving", false);
        player.playerData.anim.SetBool("walking", false);
        player.playerData.anim.SetBool("sprinting", false);
        player.playerData.anim.SetBool("crouching", false);
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        player.playerData.PlayerRb.linearVelocity = new Vector2(0,player.playerData.PlayerRb.linearVelocityY);
        if(frame > 15)
        {
            if (UserInput.Instance.KeyDownInteract || UserInput.Instance.KeyDownAttack && TextWrite.Instance._Writing == false)
            {
                //player.playerData.interacting = false;
                //TextWrite.Instance.Close();
                //player.SwitchState(player.IdleState);
            }
        }
        else
        {
            frame += 1;
        }
        
        // Falling Animation
        if (player.playerData.PlayerRb.linearVelocityY < 0) 
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
        }
        if (GroundCheck.Instance._IsGrounded)
        {
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", false);
        }
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
    }
}
