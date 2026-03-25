using UnityEngine;

public class PlayerWalking : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Walking / Walking State - " + sprinting);
        playerSpeed = 10;
        //Debug.Log(sprinting + " IN WALK STATE");
        //Debug.Log(fakeSprintToggle + " FAKE IN WALK");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, PlayerRb.linearVelocityY);
            PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, PlayerRb.linearVelocityY);
            PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        if (!moving)
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
            return;
        }
        if (jumpBufferCounter > 0)
        {
            Debug.Log("jump from walking");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
            return;
        }
        if (!GroundCheck.Instance._IsGrounded && coyoteTimeCounter < 0)
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.AirState);
            return;
        }
        if (sprinting)
        {
            player.SwitchState(player.SprintingState);
            return;
        }
    }
}
