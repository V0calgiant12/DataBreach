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
        Debug.Log("Player Idle / Idle State");
        FindPlayerObject();
        fakeSprintToggle = true;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            Debug.Log("Attacking while Idle");
        }
        if (fakeSprintToggle)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputSprint) && (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight)))
            {
                player.SwitchState(player.SprintingState);
            }
            else
            {
                if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
                {
                    player.SwitchState(player.WalkingState);
                }
            }
        }
        else
        {
            if (Input.GetKey(SettingsData.Instance._InputSprint))
            {
                player.SwitchState(player.SprintingState);
            }
            else
            {
                if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
                {
                    player.SwitchState(player.WalkingState);
                }
            }
        }
        if (Input.GetKey(SettingsData.Instance._InputDown))
        {
            player.SwitchState(player.CrouchingState);
        }
        if (jumpBufferCounter > 0)
        {
            Debug.Log("jump from idle");
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

}
