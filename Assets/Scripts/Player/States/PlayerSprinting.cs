using UnityEngine;

public class PlayerSprinting : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Sprinting / Sprinting State");
        playerSpeed = playerSpeed * 1.2f;
        GameObject Player = GameObject.Find("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
        //Switch back to idle after code is done running
    }
    public override void UpdateState(PlayerStateManager player)
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
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
