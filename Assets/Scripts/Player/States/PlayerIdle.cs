using UnityEngine;

public class PlayerIdle : PlayerAbstract
{
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player Idle / Idle State");
        Debug.Log(SettingsData.Instance._InputAttack);
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
    }
    //public override void OnCollisionEnter(PlayerStateManager player)
    //{
        //Something with PlayerStateManager's OnCollisionEnter thingy is being annoying, i'll figure it out some other time. Take sudo code for now :)
        //if (player colliding with ground) 
        //{
        //  player.SwitchState(player.AirState);
        //}
    //}
}
