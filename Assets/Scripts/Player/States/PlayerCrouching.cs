
using UnityEngine;

public class PlayerCrouching : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Player is Crouching / Crouching State");
        player.playerData.anim.SetBool("crouching", true);
        //Switch back to idle after code is done running
    }
    public override void UpdateState(PlayerStateManager player)
    {
        playerSpeed = 3 * PlayerStateManager.Instance.playerData.mudSpeedMulti;
        
        // Down attack
        if (UserInput.Instance.KeyDownAttack)
        {
            player.Attack(PlayerStateManager.AttackType.down);
        }
        // C-Stick Attacking
        if(UserInput.Instance.DirectionalAttack.y > 0.5f && UserInput.Instance.RightStickPressed)
        {
            player.Attack(PlayerStateManager.AttackType.up);
        }
        if(UserInput.Instance.DirectionalAttack.y < -0.5f && UserInput.Instance.RightStickPressed)
        {
            player.Attack(PlayerStateManager.AttackType.down);
        }
        if(UserInput.Instance.DirectionalAttack.x > 0.5f && UserInput.Instance.RightStickPressed)
        {
            player.playerData.leftOrRight = true;
            player.Attack(PlayerStateManager.AttackType.forward);
        }
        if(UserInput.Instance.DirectionalAttack.x < -0.5f && UserInput.Instance.RightStickPressed)
        {
            player.playerData.leftOrRight = false;
            player.Attack(PlayerStateManager.AttackType.forward);
        }

        // Crouch release check
        if (!player.playerData.crouching)
        {
            // Leave crouch
            player.playerData.anim.SetBool("crouching", false);
            player.SwitchState(player.IdleState);
            return;
        }

        // Crouch walking
        moving = false;
        player.playerData.anim.SetBool("moving", false);
        if (UserInput.Instance.MovementInput.x > 0.25f)
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            player.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (UserInput.Instance.MovementInput.x < -0.25f) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            player.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocity = new Vector2(0,player.playerData.PlayerRb.linearVelocityY) + player.playerData.OffsetVelocity;
        }

        // Jump check
        if (player.playerData.jumpBufferCounter > 0)
        {
            //Debug.Log("jump from Crouching");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.playerData.audioSource.PlayJumpSound(player.playerData._NormalJump);
            if (GroundCheck.Instance._IsStone)
            {
                player.playerData.audioSource.PlayStoneSound(player.playerData._StoneJump);
            }
            else
            {
                player.playerData.audioSource.PlayGrassSound(player.playerData._GrassJump);
            }
            player.SwitchState(player.AirState);
            player.playerData.anim.SetBool("crouching", false);
            return;
        }

        // Check if grounded
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            if(SettingsData.Instance._ToggleCrouch != true)
            {
                player.playerData.crouching = false;
            }
            player.SwitchState(player.AirState);
            player.playerData.anim.SetBool("crouching", false);
            return;
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
}
