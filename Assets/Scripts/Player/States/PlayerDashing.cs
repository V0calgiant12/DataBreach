using UnityEngine;

public class PlayerDashing : PlayerAbstract
{
    int dashTimer = 0;
    float storedGrav;
    bool superJump = false;
    bool attacked = false;
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        storedGrav = player.playerData.PlayerRb.gravityScale;
        player.playerData.anim.SetBool("dashing",true);
        player.playerData.anim.SetBool("superJumping",false);
        playerSpeed = 12;
        player.playerData.PlayerRb.gravityScale = 0;
        dashTimer = 20;
        player.playerData.resetVelocity = false;
        player.comingFromDash = true;
        attacked = false;
        player.playerData.PlayerRb.linearVelocity = new Vector2(20 * (player.playerData.leftOrRight? 1 : -1), jumpStrength/2);
        if(UserInput.Instance.MovementInput.y > 0.5f || player.forceSuperJump)
        {
            player.playerData.anim.SetBool("dashing",false);
            player.playerData.anim.SetBool("superJumping",true);
            superJump = true;
        }
        else
        {
            superJump = false;
        }
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        if (superJump)
        {
            JumpUpdate(player);
        }
        else
        {
            DashUpdate(player);
        }
    }
    private void DashUpdate(PlayerStateManager player)
    {
        dashTimer -= Time.timeScale == 1 ? 1:0;
        // End of Dash
        if(dashTimer <= 0 && !player.playerData.anim.GetBool("attacking"))
        {
            player.playerData.anim.SetBool("currentlyFixed",true);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
            if (!attacked)
            {
                player.playerData.PlayerRb.linearVelocityX = playerSpeed * (player.playerData.leftOrRight? 1 : -1);
            }
            else
            {
                player.playerData.anim.SetBool("currentlyFixed",false);
            }
        }

        // Switch to Super Jump if 5 or less frames in.
        if(UserInput.Instance.MovementInput.y > 0.5f && dashTimer >= 15)
        {
            player.playerData.anim.SetBool("dashing",false);
            player.playerData.anim.SetBool("superJumping",true);
            superJump = true;
        }

        // Air Dash Attack
        if(player.playerData.bufferedAtk > 0)
        {
            attacked = true;
            player.Attack(PlayerStateManager.AttackType.dashAir);
        }
        if (player.playerData.anim.GetBool("attacking"))
        {
            player.playerData.anim.SetBool("dashing",false);
            player.playerData.anim.SetBool("falling",true);
            player.playerData.PlayerRb.linearVelocityX -= Time.timeScale == 1 ? 0.3f*(player.playerData.leftOrRight? 1 : -1):0;
            player.playerData.PlayerRb.linearVelocityY -= Time.timeScale == 1 ? 0.4625f:0;
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
    private void JumpUpdate(PlayerStateManager player)
    {
        player.playerData.PlayerRb.gravityScale = storedGrav;
        player.playerData.PlayerRb.linearVelocity = new Vector2(7 * (player.playerData.leftOrRight? 1 : -1),jumpStrength*1.25f);
        player.comingFromDash = false;
        player.SwitchState(player.AirState);
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.playerData.PlayerRb.gravityScale = storedGrav;
        player.playerData.anim.SetBool("dashing",false);
        player.playerData.anim.SetBool("superJumping",false);
    }
}