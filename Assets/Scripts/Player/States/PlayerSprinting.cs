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
        playerSpeed = 25;
        GameObject Player = GameObject.Find("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
        //Switch back to idle after code is done running
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (fakeSprintToggle)
        {
        if (Input.GetKeyDown(SettingsData.Instance._InputSprint) && (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight)))
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
        }
        
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && IsGrounded())
        {
            Debug.Log("jump from Sprinting");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
        }
        else if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
        {
            player.SwitchState(player.WalkingState);
        }
        else if (!groundHit)
        {
            player.SwitchState(player.AirState);
        }
        else
        {
            PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
        }
        Debug.Log(IsGrounded());
        
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
