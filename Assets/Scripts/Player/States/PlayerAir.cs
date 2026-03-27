using UnityEngine;
using System.Collections;
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
        shakeOnLand = false;
    }
    public override void UpdateState(PlayerStateManager player)
    {

        // Fast Falling
        if (Input.GetKeyDown(SettingsData.Instance._InputDown)) // Check for fast fall.
        {
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, -jumpStrength * 1.5f);
        }
        
        // Movement
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight)) // Moving Right
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = true;
            moving = true;
        }
        else if (Input.GetKey(SettingsData.Instance._InputLeft)) // Moving left
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = false;
            moving = true;
        }
        if (!moving) // If not moving, set x velocity to 0;
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
        }

        // Attacking
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack)) // Check for an attack.
        {
            Debug.Log("Attacking while In Air");
        }

        // Short Jumping
        if(!(Input.GetKey(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKey(SettingsData.Instance._InputUp)) && player.playerData.PlayerRb.linearVelocity.y > 0)
        {
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, player.playerData.PlayerRb.linearVelocityY * 0.5f);
        }

        // Double Jumping
        if ((Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp)) && player.playerData.doubleJumpAvailable) // NOTE: Doesn't buffer the jump because we don't want the player to instantly use their double jump.
        {
            Debug.Log("jump in air");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            player.playerData.doubleJumpAvailable = false;
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
        }

        // Wall Check
        //if (WallCheck.Instance._IsClinging && moving)
        //{
        //    doubleJumpAvailable = true;
        //    player.SwitchState(player.WallClingState);
        //}
        if(PlayerStateManager.Instance.playerData.PlayerRb.linearVelocityY < -40)
        {
            shakeOnLand = true;
            //Debug.Log(shakeOnLand);
            shakeIntensityLvl = Mathf.Abs(PlayerStateManager.Instance.playerData.PlayerRb.linearVelocityY)/2 - 10;
        }
        
        // Grounded Check
        if (GroundCheck.Instance._IsGrounded)
        {
            //Debug.Log(shakeOnLand);
            if(shakeOnLand)
            {
                TriggerShake.Instance.BurstShake(shakeIntensityLvl);
            }
            player.playerData.doubleJumpAvailable = true;
            player.SwitchState(player.IdleState);
            return;
        }
    }
}
