using UnityEngine;
using System.Collections;
public class PlayerAir : PlayerAbstract
{
    private PlayerStateManager.AttackType currentAttack;
    private int downBuffer;
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
        currentAttack = PlayerStateManager.AttackType.forwardAir; // Default attack if nothing is inputed this frame.
        player.playerData.anim.SetBool("notGrounded", true);
        player.playerData.anim.SetBool("airJumping", false);
        player.playerData.anim.SetBool("falling", false);
        player.playerData.anim.SetBool("airAttacking", false);
        downBuffer -= 1;
        // Fast Falling
        if (Input.GetKeyDown(SettingsData.Instance._InputDown))
        {
            downBuffer = 10;
        }
        if (downBuffer > 0 && player.playerData.PlayerRb.linearVelocityY < 0) // Check for fast fall.
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, -jumpStrength * 1.5f);
        }
        // Check for Down Air
        if (Input.GetKey(SettingsData.Instance._InputDown))
        {
            currentAttack = PlayerStateManager.AttackType.downAir;
        }
        // Check for Up Air
        if (Input.GetKey(SettingsData.Instance._InputUp))
        {
            currentAttack = PlayerStateManager.AttackType.upAir;
        }
        
        // Movement left/right and sets attack.
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight)) // Moving Right
        {
            if (player.playerData.leftOrRight)
            {
                currentAttack = PlayerStateManager.AttackType.forwardAir;
            }
            else
            {
                currentAttack = PlayerStateManager.AttackType.backAir;
            }
            
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) // Moving left
        {
            if (player.playerData.leftOrRight)
            {
                currentAttack = PlayerStateManager.AttackType.backAir;
            }
            else
            {
                currentAttack = PlayerStateManager.AttackType.forwardAir;
            }
            currentAttack = PlayerStateManager.AttackType.forwardAir;
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
        }
        if (!moving) // If not moving, set x velocity to 0;
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
        }
        if (player.playerData.PlayerRb.linearVelocityY < 0) 
        {
            player.playerData.anim.SetBool("falling", true);
        }

        // Attacking
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack)) // Check for an attack.
        {
            Debug.Log("Attacking while In Air");
            player.playerData.audioSource.PlayPlayerAttackSound(player.playerData._PlayerAttack);
            player.playerData.anim.SetBool("airAttacking", true);
            player.Attack(currentAttack);
            if (player.playerData.PlayerRb.linearVelocityY < 0) 
            {
                player.playerData.anim.SetBool("falling", true);
            }
        }

        // Short Jumping
        if(!(Input.GetKey(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKey(SettingsData.Instance._InputUp)) && player.playerData.PlayerRb.linearVelocity.y > 0)
        {
            player.playerData.anim.SetBool("airJumping", true);
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, player.playerData.PlayerRb.linearVelocityY * 0.5f);
        }

        // Double Jumping
        if ((Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp)) && player.playerData.doubleJumpAvailable) // NOTE: Doesn't buffer the jump because we don't want the player to instantly use their double jump.
        {
            if (Input.GetKey(SettingsData.Instance._InputRight))
            {
                player.playerData.leftOrRight = true;
            }
            if (Input.GetKey(SettingsData.Instance._InputLeft))
            {
                player.playerData.leftOrRight = false;
            }
            Debug.Log("jump in air");
            player.playerData.audioSource.PlayJumpSound(player.playerData._AirJump);
            player.playerData.anim.SetBool("airJumping", true);
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
        if(player.playerData.PlayerRb.linearVelocityY < -40)
        {
            shakeOnLand = true;
            //Debug.Log(shakeOnLand);
            shakeIntensityLvl = Mathf.Abs(player.playerData.PlayerRb.linearVelocityY)/2 - 10;
        }
        
        // Grounded Check
        if (GroundCheck.Instance._IsGrounded)
        {
            player.playerData.anim.SetBool("notGrounded", false);
            //Debug.Log(shakeOnLand);
            if(shakeOnLand)
            {
                TriggerShake.Instance.BurstShake(shakeIntensityLvl);
            }
            player.playerData.doubleJumpAvailable = true;
            player.playerData.audioSource.PlayJumpSound(player.playerData._NormalFall);
            if (GroundCheck.Instance._IsStone)
            {
                player.playerData.audioSource.PlayStoneSound(player.playerData._StoneFall);
            }
            else
            {
                player.playerData.audioSource.PlayGrassSound(player.playerData._GrassFall);
            }
            player.SwitchState(player.IdleState);
            return;
        }
    }
}
