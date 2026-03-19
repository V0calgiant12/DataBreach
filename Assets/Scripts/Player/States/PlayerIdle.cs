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
        GameObject Player = GameObject.Find("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
    }
    public override void UpdateState(PlayerStateManager player)
    {
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
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && IsGrounded())
        {
            Debug.Log("jump");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX,3f);
        }
        Debug.Log(IsGrounded());
        Debug.DrawRay(groundHit.point, groundHit.normal, Color.red);
    }

}
