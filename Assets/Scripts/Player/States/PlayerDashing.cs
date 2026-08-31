using UnityEngine;

public class PlayerDashing : PlayerAbstract
{
    int dashTimer = 0;
    float storedGrav;
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        storedGrav = player.playerData.PlayerRb.gravityScale;
        playerSpeed = 12;
        player.playerData.PlayerRb.gravityScale = 0;
        dashTimer = 20;
        player.playerData.resetVelocity = false;
        player.comingFromDash = true;
        player.playerData.PlayerRb.linearVelocity = new Vector2(20 * (player.playerData.leftOrRight? 1 : -1), jumpStrength/2);
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        dashTimer -= Time.timeScale == 1 ? 1:0;
        if(dashTimer <= 0)
        {
            player.SwitchState(player.AirState);
            player.playerData.PlayerRb.linearVelocityX = playerSpeed * (player.playerData.leftOrRight? 1 : -1);
        }

        // Early Cancel (from external factors)
        {
            // Hit a wall
            if(player.playerData.ricochet > 0)
            {
                Debug.Log("Early Cancel");
                player.SwitchState(player.AirState);
                //player.playerData.PlayerRb.linearVelocityX = playerSpeed ;
            }
            // Ground Check
            if (GroundCheck.Instance._IsGrounded && dashTimer < 15)
            {
                Debug.Log("Early Cancel");
                player.playerData.audioSource.PlayJumpSound(player.playerData._NormalFall);
                if (GroundCheck.Instance._IsStone)
                {
                    player.playerData.audioSource.PlayStoneSound(player.playerData._StoneFall);
                }
                else
                {
                    player.playerData.audioSource.PlayGrassSound(player.playerData._GrassFall);
                }
                if((player.playerData.anim.GetInteger("attackId") == 2 || player.playerData.anim.GetInteger("attackId") == 4) && player.playerData.anim.GetBool("attacking"))
                {
                    player.playerData.anim.SetBool("attacking", false);
                }
                player.SwitchState(player.IdleState);
                player.playerData.anim.SetBool("falling", false);
                player.playerData.anim.SetBool("jumping", false);
                return;
            }
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.playerData.PlayerRb.gravityScale = storedGrav;
    }
}