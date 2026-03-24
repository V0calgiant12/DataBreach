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
        playerSpeed = 7;
    }
    public override void UpdateState(PlayerStateManager player)
    {

        // Fast Falling
        if (Input.GetKeyDown(SettingsData.Instance._InputDown)) // Check for fast fall.
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

        // Short Jumping
        if((Input.GetKeyUp(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyUp(SettingsData.Instance._InputUp)) && PlayerRb.linearVelocity.y > 0)
        {
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, PlayerRb.linearVelocityY * 0.8f);
        }

        // Double Jumping
        if ((Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp)) && doubleJumpAvailable) // NOTE: Doesn't buffer the jump because we don't want the player to instantly use their double jump.
        {
            Debug.Log("jump in air");
            PlayerRb.linearVelocity = new Vector2(PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            doubleJumpAvailable = false;
            jumpBufferCounter = 0;
            coyoteTimeCounter = 0;
        }

        // Wall Check
        //if (WallCheck.Instance._IsClinging && moving)
        //{
        //    doubleJumpAvailable = true;
        //    player.SwitchState(player.WallClingState);
        //}

        // Grounded Check
        if (GroundCheck.Instance._IsGrounded)
        {
            doubleJumpAvailable = true;
            player.SwitchState(player.IdleState);
        }
    }
    
    //publi override void OnCollisionEnter(PlayerStateManager player)
    //
    
    //}
}
