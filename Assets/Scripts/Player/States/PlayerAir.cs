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
        FindPlayerObject();
        playerSpeed = 7;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();

        if (Input.GetKey(SettingsData.Instance._InputDown))
        {
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, -10f);
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            Debug.Log("Attacking while In Air");
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
        {
            if (Input.GetKey(SettingsData.Instance._InputRight))
            {
                PlayerVelocity = new Vector2(playerSpeed, PlayerRb.linearVelocityY);
                PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            }
            else if (Input.GetKey(SettingsData.Instance._InputLeft)) 
            {
                PlayerVelocity = new Vector2(-playerSpeed, PlayerRb.linearVelocityY);
                PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            }
        }
        if (!moving)
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && GroundCheck.Instance._IsGrounded)
        {
            Debug.Log("jump from idle");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
        }
        if (GroundCheck.Instance._IsGrounded)
        {
            player.SwitchState(player.IdleState);
        }
    }
    
    //publi override void OnCollisionEnter(PlayerStateManager player)
    //
    
    //}
}
