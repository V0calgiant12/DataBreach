using UnityEngine;

public class PlayerAir : PlayerAbstract
{
public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is in the air / Air State");
        FindPlayerObject();
        playerSpeed = 7;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        FindPlayerObject();

        // Fast Falling
        if (Input.GetKey(SettingsData.Instance._InputDown)) // Check for fast fall.
        {
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, -jumpStrength * 2);
        }
        
        // Movement
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight)) // Moving Right
        {
            PlayerVelocity = new Vector2(playerSpeed, PlayerRb.linearVelocityY);
            PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        else if (Input.GetKey(SettingsData.Instance._InputLeft)) // Moving left
        {
            PlayerVelocity = new Vector2(-playerSpeed, PlayerRb.linearVelocityY);
            PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        if (!moving) // If not moving, set x velocity to 0;
        {
            PlayerRb.linearVelocityX = 0;
        }

        // Attacking
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack)) // Check for an attack.
        {
            Debug.Log("Attacking while In Air");
        }

        // Jumping
        if (Input.GetKeyDown(SettingsData.Instance._InputJump) && doublJumpAvailable)
        {
            Debug.Log("jump in air");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            doublJumpAvailable = false;
        }
        if (GroundCheck.Instance._IsGrounded)
        {
            doublJumpAvailable = true;
            player.SwitchState(player.IdleState);
        }
    }
    
    //publi override void OnCollisionEnter(PlayerStateManager player)
    //
    
    //}
}
