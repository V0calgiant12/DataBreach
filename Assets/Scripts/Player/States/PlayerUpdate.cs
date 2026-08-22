using UnityEngine;

public class PlayerUpdate : PlayerAbstract
{
    bool directionaAttackCrouch = false;
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        player.playerData.MainCamera.GetComponent<Camera>().orthographicSize = SettingsData.Instance._CameraZoom;
        
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        // Set jump buffer if pressed
        if(UserInput.Instance.KeyDownJump || SettingsData.Instance._UpToJump && UserInput.Instance.KeyDownUpInput)
        {
            //Debug.Log("Jump");
            player.playerData.jumpBufferCounter = 10;
        }
        
        // Toggle sprint
        if (UserInput.Instance.KeyDownSprint && SettingsData.Instance._ToggleSprint)
        {
            //Debug.Log("Toggle sprint " + SettingsData.Instance._ToggleSprint);
            player.playerData.sprinting = !player.playerData.sprinting;
        }
        // No toggle sprint
        if (SettingsData.Instance._ToggleSprint == false && UserInput.Instance.KeyHeldDownSprint)
        {
            //Debug.Log("Holding Sprint " + SettingsData.Instance._ToggleSprint);
            player.playerData.sprinting = true;
        }
        else if (SettingsData.Instance._ToggleSprint == false && !UserInput.Instance.KeyHeldDownSprint)
        {
            //Debug.Log("Let go of sprint " + SettingsData.Instance._ToggleSprint);
            player.playerData.sprinting = false;
        }

        // Crouching
        if (SettingsData.Instance._ToggleCrouch)
        {
            // Crouch toggle on
            if (UserInput.Instance.KeyDownCrouch)
            {
                player.playerData.crouching = !player.playerData.crouching;
            }
        }
        else
        {
            // Crouch toggle off
            if (SettingsData.Instance._ToggleCrouch == false && UserInput.Instance.MovementInput.y < -0.4f)
            {
                player.playerData.crouching = true;
            }
            if (SettingsData.Instance._ToggleCrouch == false && UserInput.Instance.MovementInput.y > -0.4f)
            {
                player.playerData.crouching = false;
            }
        }
        if (UserInput.Instance.DirectionalAttack.y < -0.5f)
        {
            player.playerData.crouching = true;
            directionaAttackCrouch = true;
        }
        if (UserInput.Instance.DirectionalAttack.y > -0.25f && directionaAttackCrouch)
        {
            player.playerData.crouching = false;
            directionaAttackCrouch = false;
        }
        if (player.playerData.playerHealth <= 0 && GroundCheck.Instance._IsGrounded)
        {
            player.SwitchState(player.DeadState);
            return;
        }

        // Attack Buffering
        if (UserInput.Instance.KeyDownAttack)
        {
            player.playerData.bufferedAtk = 10;
            player.playerData.bufferedAtkDir = new Vector2(0,0);
        }
        // C-Stick Attack Buffering
        if((UserInput.Instance.DirectionalAttack.y < -0.5f || UserInput.Instance.DirectionalAttack.y > 0.5f || UserInput.Instance.DirectionalAttack.x < -0.5f || UserInput.Instance.DirectionalAttack.x > 0.5f) && UserInput.Instance.RightStickPressed)
        {
            player.playerData.bufferedAtk = 10;
            player.playerData.bufferedAtkDir = UserInput.Instance.DirectionalAttack;
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
}