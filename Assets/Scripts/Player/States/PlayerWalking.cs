using UnityEngine;

public class PlayerWalking : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Walking / Walking State");
        playerSpeed = 10;
        FindPlayerObject();
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        if (fakeSprintToggle)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputSprint))
            {
                player.SwitchState(player.SprintingState);
            }
        }
        else
        {
            if (Input.GetKey(SettingsData.Instance._InputSprint))
            {
                player.SwitchState(player.SprintingState);
            }
        }
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
        }
        if (jumpBufferCounter > 0)
        {
            Debug.Log("jump from walking");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
        }
        if (!GroundCheck.Instance._IsGrounded && coyoteTimeCounter < 0)
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.AirState);
        }

        
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
