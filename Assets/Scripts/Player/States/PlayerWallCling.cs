using UnityEngine;
using System;

public class PlayerWallCling : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is clinging to wall / Wall Cling State");
        FindPlayerObject();
        playerSpeed = 7;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        // Wall Slide
        PlayerRb.linearVelocity = new Vector2(0, -3);

        // Wall Jumping
        if ((Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp)) && doubleJumpAvailable) // NOTE: Doesn't buffer the jump because we don't want the player to instantly use their wall jump.
        {
            Debug.Log("Wall jump");
            if (currentWallSide && Convert.ToInt16(currentWallSide) != lastWallJumpRight)
            {
                PlayerRb.linearVelocity = new Vector2(-10, jumpStrength);
                lastWallJumpRight = 1;
            }
            else if(!currentWallSide && Convert.ToInt16(currentWallSide) != lastWallJumpRight)
            {
                PlayerRb.linearVelocity = new Vector2(10, jumpStrength);
                lastWallJumpRight = 0;
            }
            doubleJumpAvailable = false;
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }

        // No input
        if (!Input.GetKey(SettingsData.Instance._InputLeft) || !Input.GetKey(SettingsData.Instance._InputRight))
        {
            doubleJumpAvailable = true;
            player.SwitchState(player.AirState);
        }

        // Grounded Check
        if (GroundCheck.Instance._IsGrounded)
        {
            doubleJumpAvailable = true;
            player.SwitchState(player.IdleState);
        }
    }
}