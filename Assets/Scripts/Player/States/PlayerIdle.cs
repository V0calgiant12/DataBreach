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
        Debug.Log(PlayerStateManager.Instance.playerData.playerHealth <= 0);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        player.playerData.PlayerRb.linearVelocityX = 0;
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            Debug.Log("Attacking while Idle");
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
        {
            player.SwitchState(player.WalkingState);
            return;
        }
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            return;
        }
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from idle");
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
        if (PlayerStateManager.Instance.playerData.playerHealth <= 0)
        {
            player.SwitchState(player.DeadState);
        }
    }

}
