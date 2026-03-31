using UnityEngine;

public class PlayerCrouching : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Crouching / Crouching State");
        playerSpeed = 3;
        //Switch back to idle after code is done running]
    }
    public override void UpdateState(PlayerStateManager player)
    {
        // Down attack
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            player.playerData.audioSource.PlayPlayerAttackSound(player.playerData._PlayerAttack);
            player.Attack(PlayerStateManager.AttackType.down);
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
        player.playerData.anim.SetBool("crouching", true);
        player.playerData.anim.SetBool("moving", false);
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = true;
            player.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = false;
            player.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
        }

        // Jump check
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from Crouching");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * 0.8f);
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
            return;
        }
    }
}
