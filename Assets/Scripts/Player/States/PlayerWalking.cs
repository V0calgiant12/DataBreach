using UnityEngine;

public class PlayerWalking : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Walking / Walking State");
        playerSpeed = 10;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (fakeSprintToggle)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputSprint))
            {
                sprintToggler = true;
                player.SwitchState(player.SprintingState);
            }
        }
        else
        {
            if (Input.GetKey(SettingsData.Instance._InputSprint))
            {
                player.SwitchState(player.SprintingState);
            }
        }
        
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && IsGrounded())
        {
            Debug.Log("jump from walking");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
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
        else if (!groundHit)
        {
            PlayerRb.linearVelocityX = 0;
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
