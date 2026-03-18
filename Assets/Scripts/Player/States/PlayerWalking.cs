using UnityEngine;

public class PlayerWalking : PlayerAbstract
{

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Walking / Walking State");
        GameObject Player = GameObject.Find("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKey(SettingsData.Instance._InputSprint))
            {
                player.SwitchState(player.SprintingState);
            }
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
