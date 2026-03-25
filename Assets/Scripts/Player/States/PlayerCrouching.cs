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
        playerSpeed = 5;
        //Switch back to idle after code is done running]
    }
    public override void UpdateState(PlayerStateManager player)
    {

        // Crouch release check
        if (!player.playerData.crouching)
        {
            // Leave crouch
            player.SwitchState(player.IdleState);
            return;
        }

        // Crouch walking
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
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
            player.SwitchState(player.AirState);
            return;
        }

        // Check if grounded
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            player.SwitchState(player.AirState);
            return;
        }
    }
}
