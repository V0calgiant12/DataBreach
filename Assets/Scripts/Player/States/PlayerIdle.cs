using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerIdle : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Player Idle / Idle State");
        lastWallJumpRight = -1;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            Debug.Log("Attacking while Idle");
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
        {
            player.SwitchState(player.WalkingState);
            return;
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputDown))
        {
            player.SwitchState(player.CrouchingState);
            return;
        }
        if (jumpBufferCounter > 0)
        {
            Debug.Log("jump from idle");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength);
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
            return;
        }
        if (!GroundCheck.Instance._IsGrounded && coyoteTimeCounter < 0)
        {
            player.SwitchState(player.AirState);
            return;
        }
    }

}
