using UnityEngine;

public class PlayerCrouching : PlayerAbstract
{
    private int audioTimer = 0;
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

        // Crouch release check
        if (!player.playerData.crouching)
        {
            // Leave crouch
            PlayerStateManager.Instance.playerData.anim.SetBool("crouching", false);
            player.SwitchState(player.IdleState);
            return;
        }

        // Crouch walking
        moving = false;
        PlayerStateManager.Instance.playerData.anim.SetBool("crouching", true);
        PlayerStateManager.Instance.playerData.anim.SetBool("moving", false);
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = true;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = false;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
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
