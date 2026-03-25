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
        if (fakeCrouchToggle)
        {
            // Crouch toggle on
            if (Input.GetKeyDown(SettingsData.Instance._InputDown))
            {
                player.SwitchState(player.IdleState);
                return;
            }
        }
        else
        {
            // Crouch toggle off
            if (Input.GetKeyUp(SettingsData.Instance._InputDown))
            {
                player.SwitchState(player.IdleState);
                return;
                
            }
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
