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
        FindPlayerObject();
        //Switch back to idle after code is done running]
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        

        // Crouch release check
        if (fakeCrouchToggle)
        {
            // Crouch toggle on
            if (Input.GetKeyDown(SettingsData.Instance._InputDown))
            {
                player.SwitchState(player.IdleState);
            }
        }
        else
        {
            // Crouch toggle off
            if (Input.GetKeyUp(SettingsData.Instance._InputDown))
            {
                player.SwitchState(player.IdleState);
                
            }
        }
        
        // Jump check
        if (jumpBufferCounter > 0)
        {
            Debug.Log("jump from Crouching");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
        }

        // Check if grounded
        if (!GroundCheck.Instance._IsGrounded && coyoteTimeCounter < 0)
        {
            player.SwitchState(player.AirState);
        }
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
