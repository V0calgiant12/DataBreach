using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal.Filters;
public class PlayerAir : PlayerAbstract
{
    private PlayerStateManager.AttackType currentAttack;
    private int fallTimer;
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Player is in the air / Air State");
        playerSpeed = player.comingFromDash ? 12:7;
        Debug.Log(player.comingFromDash);
        fallTimer = 0;
        shakeOnLand = false;
        player.playerData.fastFallCounter = 0;
        if (player.playerData.PlayerRb.linearVelocityY > 0) 
        {
            player.StartCoroutine(player.WaitUntilNotJumping());
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", true);
        }
        if (player.playerData.PlayerRb.linearVelocityY < 0) 
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
        }
        
        if(player.playerData.jumpBufferCounter < -5)
        {
            player.playerData.coyoteTimeCounter = 15;
        }
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if(playerSpeed > 7)
        {
            playerSpeed -= 0.05f;
            if(!player.playerData.resetVelocity)
            {
                player.playerData.PlayerRb.linearVelocityX -= 0.05f;
            }
        }
        player.playerData.fastFallCounter -= 1;
        // Fast Falling
        if (UserInput.Instance.KeyDownCrouch && player.playerData.PlayerRb.linearVelocityY < 0)
        {
            player.playerData.resetVelocity = true;
            if (SettingsData.Instance._DoubleTapFastFall && player.playerData.fastFallCounter > 0)
            {
                player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, -jumpStrength * 1.5f);
                player.playerData.fastFallCounter = 0;
            }
            else if (!SettingsData.Instance._DoubleTapFastFall)
            {
                player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, -jumpStrength * 1.5f);
            }
        }
        if (SettingsData.Instance._DoubleTapFastFall && UserInput.Instance.KeyDownCrouch)
        {
            player.playerData.fastFallCounter = 45;
        }
        
        // Default to Forward Air if nothing else is inputed. (If anything else is inputed, this will be overwritten.)
        currentAttack = PlayerStateManager.AttackType.forwardAir;
        
        // Movement left/right and sets attack.
        moving = false;
        if (UserInput.Instance.MovementInput.x > 0.25f) // Moving Right
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
            player.playerData.resetVelocity = true;
        }
        if (UserInput.Instance.MovementInput.x < -0.25f) // Moving left
        {
            if (player.playerData.leftOrRight)
            {
                currentAttack = PlayerStateManager.AttackType.backAir;
            }
            else
            {
                currentAttack = PlayerStateManager.AttackType.forwardAir;
            }
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            moving = true;
            player.playerData.resetVelocity = true;
        }
        if (UserInput.Instance.MovementInput.x < 0.25f && UserInput.Instance.MovementInput.x > -0.25f && player.playerData.resetVelocity) // If not moving, set x velocity to 0;
        {
            {
                player.playerData.PlayerRb.linearVelocityX = 0;
            }
        }
        if (player.playerData.PlayerRb.linearVelocityY < 0) 
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
            player.playerData.inAirGust = false;
        }
        
        // Check for Down Air
        if (UserInput.Instance.MovementInput.y < -0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.downAir;
        }
        else if (UserInput.Instance.MovementInput.y < 0 && UserInput.Instance.MovementInput.x > -0.5f && UserInput.Instance.MovementInput.x < 0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.downAir;
        }
        // Check for Up Air
        if (UserInput.Instance.MovementInput.y > 0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.upAir;
        }
        else if (UserInput.Instance.MovementInput.y > 0 && UserInput.Instance.MovementInput.x > -0.5f && UserInput.Instance.MovementInput.x < 0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.upAir;
        }

        // Attacking
        if (player.playerData.bufferedAtk > 0) // Check for an attack.
        {
            // Button Press
            if(player.playerData.bufferedAtkDir == new Vector2(0, 0))
            {
                player.Attack(currentAttack);
            }
            // C-Stick Attacking
            if(player.playerData.bufferedAtkDir.y > 0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.upAir;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.y < -0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.downAir;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.x > 0.5f)
            {
                currentAttack = player.playerData.leftOrRight ? PlayerStateManager.AttackType.forwardAir : PlayerStateManager.AttackType.backAir;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.x < -0.5f)
            {
                currentAttack = player.playerData.leftOrRight ? PlayerStateManager.AttackType.backAir :  PlayerStateManager.AttackType.forwardAir;
                player.Attack(currentAttack);
            }
        }

        // Short Jumping

        if(!(UserInput.Instance.KeyHeldDownJump || SettingsData.Instance._UpToJump && UserInput.Instance.MovementInput.y > 0.5f) && player.playerData.PlayerRb.linearVelocity.y > 0 && !player.playerData.inAirGust && player.isJumping)
        {
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, player.playerData.PlayerRb.linearVelocityY * 0.5f);
        }

        // Double Jumping
        if (player.playerData.jumpBufferCounter > 0 && player.playerData.doubleJumpAvailable && player.playerData.coyoteTimeCounter < 0) // NOTE: Doesn't buffer the jump because we don't want the player to instantly use their double jump.
        {
            player.playerData.jumpBufferCounter = 0;
            if (UserInput.Instance.MovementInput.x > 0.25f)
            {
                player.playerData.leftOrRight = true;
            }
            if (UserInput.Instance.MovementInput.x < -0.25f)
            {
                player.playerData.leftOrRight = false;
            }
            //Debug.Log("jump in air");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            player.StartCoroutine(player.WaitUntilNotJumping());
            player.playerData.audioSource.PlayJumpSound(player.playerData._AirJump);
            player.playerData.anim.SetBool("jumping", true);
            player.playerData.doubleJumpAvailable = false;
            player.playerData.coyoteTimeCounter = 0;
        }

        // Wall Check
        //if (WallCheck.Instance._IsClinging && moving)
        //{
        //    doubleJumpAvailable = true;
        //    player.SwitchState(player.WallClingState);
        //}

        // Fall Timer
        if(player.playerData.PlayerRb.linearVelocityY > 0)
        {
            fallTimer = 0;
        }
        if(player.playerData.PlayerRb.linearVelocityY < 0)
        {
            fallTimer += Time.timeScale == 1 ? 1 : 0;
        }
        
        if(fallTimer > 45)
        {
            shakeOnLand = true;
            shakeIntensityLvl = fallTimer/5 + Mathf.Abs(player.playerData.PlayerRb.linearVelocityY)/4;
        }

        // Grounded Jump check for Coyote time.
        if (player.playerData.jumpBufferCounter > 0 && player.playerData.coyoteTimeCounter > 0)
        {
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", true);
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.playerData.audioSource.PlayJumpSound(player.playerData._NormalJump);
            if (GroundCheck.Instance._IsStone)
            {
                player.playerData.audioSource.PlayStoneSound(player.playerData._StoneJump);
            }
            else
            {
                player.playerData.audioSource.PlayGrassSound(player.playerData._GrassJump);
            }
        }
        
        // Grounded Check
        if (GroundCheck.Instance._IsGrounded)
        {
            if(shakeOnLand)
            {
                TriggerShake.Instance.BurstShake(shakeIntensityLvl,1,true,0);
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
            if((player.playerData.anim.GetInteger("attackId") == 2 || player.playerData.anim.GetInteger("attackId") == 4) && player.playerData.anim.GetBool("attacking"))
            {
                player.playerData.anim.SetBool("attacking", false);
            }
            player.playerData.fastFallCounter = 0;
            player.SwitchState(player.IdleState);
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", false);
            return;
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        
    }
    public override void LeaveState(PlayerStateManager player)
    {
        Debug.Log("Air Leave");
        player.comingFromDash = false;
    }
}
