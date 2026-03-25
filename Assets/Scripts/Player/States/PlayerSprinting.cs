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
        playerSpeed = 25f;
        //Debug.Log(sprinting + " IN SPRINT STATE");
        //Debug.Log(fakeSprintToggle + " FAKE IN SPRINT");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        moving = false;
        // sprint right
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        // sprint left
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
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
