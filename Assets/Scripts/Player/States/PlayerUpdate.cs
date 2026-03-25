using UnityEngine;

public class PlayerUpdate : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        // Counter countdowns
        player.playerData.jumpBufferCounter -= 1;
        player.playerData.coyoteTimeCounter -= 1;

        // Set jump buffer if pressed
        if(Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp))
        {
            Debug.Log("Jump");
            player.playerData.jumpBufferCounter = 5;
        }
        // Toggle sprint
        if (Input.GetKeyDown(SettingsData.Instance._InputSprint) && player.playerData.fakeSprintToggle)
        {
            Debug.Log("Toggle sprint " + player.playerData.fakeSprintToggle);
            player.playerData.sprinting = !player.playerData.sprinting;
        }
        // No toggle sprint
        if (player.playerData.fakeSprintToggle == false && Input.GetKeyDown(SettingsData.Instance._InputSprint))
        {
            Debug.Log("Holding Sprint " + player.playerData.fakeSprintToggle);
            player.playerData.sprinting = true;
        }
        else if (player.playerData.fakeSprintToggle == false && Input.GetKeyUp(SettingsData.Instance._InputSprint))
        {
            Debug.Log("Let go of sprint " + player.playerData.fakeSprintToggle);
            player.playerData.sprinting = false;
        }

        // Crouching
        if (player.playerData.fakeCrouchToggle)
        {
            // Crouch toggle on
            if (Input.GetKeyDown(SettingsData.Instance._InputDown))
            {
                player.playerData.crouching = !player.playerData.crouching;
            }
        }
        else
        {
            // Crouch toggle on
            if (Input.GetKeyDown(SettingsData.Instance._InputDown))
            {
                player.playerData.crouching = true;
            }
            // Crouch toggle off
            if (Input.GetKeyUp(SettingsData.Instance._InputDown))
            {
                player.playerData.crouching = false;
            }
        }
    }
}
