using UnityEngine;

public class PlayerUpdate : PlayerAbstract
{
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        player.playerData.MainCamera.GetComponent<Camera>().orthographicSize = SettingsData.Instance._CameraZoom;
        
        player.playerData.sprinting = false;
        player.playerData.crouching = false;
        player.playerData.movementAllowed = true;
        player.playerData.leftOrRight = true;
        player.playerData.playerHealth = 5;
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    { 
        // Counter countdowns
        player.playerData.jumpBufferCounter -= 1;
        player.playerData.coyoteTimeCounter -= 1;

        // Set jump buffer if pressed
        if(Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp))
        {
            //Debug.Log("Jump");
            player.playerData.jumpBufferCounter = 10;
        }
        
        // Toggle sprint
        if (Input.GetKeyDown(SettingsData.Instance._InputSprint) && SettingsData.Instance._ToggleSprint)
        {
            //Debug.Log("Toggle sprint " + SettingsData.Instance._ToggleSprint);
            player.playerData.sprinting = !player.playerData.sprinting;
        }
        // No toggle sprint
        if (SettingsData.Instance._ToggleSprint == false && Input.GetKeyDown(SettingsData.Instance._InputSprint))
        {
            //Debug.Log("Holding Sprint " + SettingsData.Instance._ToggleSprint);
            player.playerData.sprinting = true;
        }
        else if (SettingsData.Instance._ToggleSprint == false && Input.GetKeyUp(SettingsData.Instance._InputSprint))
        {
            //Debug.Log("Let go of sprint " + SettingsData.Instance._ToggleSprint);
            player.playerData.sprinting = false;
        }

        // Crouching
        if (GroundCheck.Instance._IsGrounded)
        {
            if (SettingsData.Instance._ToggleCrouch)
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
            if (PlayerStateManager.Instance.playerData.playerHealth <= 0)
            {
                player.SwitchState(player.DeadState);
                return;
            }
        }
    }
}
