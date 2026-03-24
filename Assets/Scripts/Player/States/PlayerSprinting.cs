using UnityEngine;

public class PlayerSprinting : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Sprinting / Sprinting State");
        playerSpeed = 25f;
        FindPlayerObject();
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        if (fakeSprintToggle && sprinting)
        {
            if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
            {
                if (Input.GetKey(SettingsData.Instance._InputRight))
                {
                    PlayerVelocity = new Vector2(playerSpeed, PlayerRb.linearVelocityY);
                    PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
                }
                if (Input.GetKey(SettingsData.Instance._InputLeft)) 
                {
                    PlayerVelocity = new Vector2(-playerSpeed, PlayerRb.linearVelocityY);
                    PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
                }
        }
        else if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
        {
            player.SwitchState(player.WalkingState);
        }
        else
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
        }
        }
        else
        {
            if (Input.GetKey(SettingsData.Instance._InputSprint) && (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight)))
            {
                if (Input.GetKey(SettingsData.Instance._InputRight))
                {
                    PlayerVelocity = new Vector2(playerSpeed, PlayerRb.linearVelocityY);
                    PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
                }
            if (Input.GetKey(SettingsData.Instance._InputLeft)) 
                {
                    PlayerVelocity = new Vector2(-playerSpeed, PlayerRb.linearVelocityY);
                    PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
                }
            }
            else if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
            {
                player.SwitchState(player.WalkingState);
            }
            else
            {
                PlayerRb.linearVelocityX = 0;
                player.SwitchState(player.IdleState);
            }
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && GroundCheck.Instance._IsGrounded)
        {
            Debug.Log("jump from Sprinting");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
        }
        if (!GroundCheck.Instance._IsGrounded)
        {
            player.SwitchState(player.AirState);
        }
        Debug.Log(GroundCheck.Instance._IsGrounded);
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
