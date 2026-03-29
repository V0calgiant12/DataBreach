using UnityEngine;

public class PlayerSprinting : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Sprinting / Sprinting State - " + player.playerData.sprinting);
        playerSpeed = 15f;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        moving = false;
        PlayerStateManager.Instance.playerData.anim.SetBool("moving", false);
        PlayerStateManager.Instance.playerData.anim.SetBool("sprinting", true);
        // sprint right
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = true;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        // sprint left
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = false;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        // if crouching go to crouching
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            return;
        }
        // if not moving then go to idle
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
            return;
        }
        // if not sprinting go to walking 
        if (player.playerData.sprinting == false)
        {
            PlayerStateManager.Instance.playerData.anim.SetBool("sprinting", false);
            Debug.Log(player.playerData.sprinting);
            player.SwitchState(player.WalkingState);
            return;
        }
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from Sprinting");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
            return;
        }
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            player.SwitchState(player.AirState);
            return;
        }
    }
}
