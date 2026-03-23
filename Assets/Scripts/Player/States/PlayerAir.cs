using UnityEngine;

public class PlayerAir : PlayerAbstract
{
public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is in the air / Air State");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log(IsGrounded());
        if (!groundHit)
        {
            if (Input.GetKey(SettingsData.Instance._InputDown))
            {
                PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, -10f);
            }
            else
            {
                if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
                {
                Debug.Log("Attacking while In Air");
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
                if (Input.GetKeyDown(SettingsData.Instance._InputJump) && IsGrounded())
                {
                    Debug.Log("jump from idle");
                    PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
                }
            }
        }
        else
        {
            player.SwitchState(player.IdleState);
        }
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
