using UnityEngine;

public class PlayerCrouching : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Crouching / Crouching State");
        playerSpeed = 5;
        FindPlayerObject();
        //Switch back to idle after code is done running]
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();
        if (!IsGrounded())
        {
            player.SwitchState(player.AirState);
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && IsGrounded())
        {
            Debug.Log("jump from Crouching");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, 10f);
        }
        if (fakeCrouchToggle)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputDown))
            {
                //crouch code here
            }
            else
            {
                player.SwitchState(player.IdleState);
            }
        }
        else
        {
            if (Input.GetKey(SettingsData.Instance._InputDown))
            {
                //crouch code here
            }
            else
            {
                player.SwitchState(player.IdleState);
            }
        }
        Debug.Log(IsGrounded());
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        
    //}
}
