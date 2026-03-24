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
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            Debug.Log("Attacking while Idle");
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
        {
            if (Input.GetKey(SettingsData.Instance._InputSprint))
            {
                player.SwitchState(player.SprintingState);
            }
            else
            {
                player.SwitchState(player.WalkingState);
            }
        }
        if (Input.GetKey(SettingsData.Instance._InputDown))
        {
            player.SwitchState(player.CrouchingState);
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputJump))
        {
            Debug.Log("jump from idle");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
        }
        if (!GroundCheck.Instance._IsGrounded)
        {
            player.SwitchState(player.AirState);
        }
        //Debug.Log(GroundCheck.Instance._IsGrounded);
    }

}
