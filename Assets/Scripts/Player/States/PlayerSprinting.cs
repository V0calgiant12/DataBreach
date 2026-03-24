using UnityEngine;

public class PlayerSprinting : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Sprinting / Sprinting State");
        playerSpeed = 25f;
        Debug.Log(sprinting + " IN SPRINT STATE");
        Debug.Log(fakeSprintToggle + " FAKE IN SPRINT");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        moving = false;
        // sprint right
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, PlayerRb.linearVelocityY);
            PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        // sprint left
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, PlayerRb.linearVelocityY);
            PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        // if not moving then go to idle
        if (!moving)
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
        }
        // if not sprinting go to walking 
        //Debug.Log(sprinting + " sprint");
        if (!sprinting && moving)
        {
            player.SwitchState(player.WalkingState);
        }

        if (jumpBufferCounter > 0)
        {
            Debug.Log("jump from Sprinting");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
        }
        if (!GroundCheck.Instance._IsGrounded && coyoteTimeCounter < 0)
        {
            player.SwitchState(player.AirState);
        }
        //Debug.Log(GroundCheck.Instance._IsGrounded);
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
