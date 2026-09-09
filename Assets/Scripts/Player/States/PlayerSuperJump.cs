using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal.Filters;
public class PlayerSuperJump : PlayerAbstract
{
    private PlayerStateManager.AttackType currentAttack;
    private float yFallStart;
    private bool changeYStartNextFall = true;
    private bool pastFirstFrame = false;
    private bool initialJump = true;
    private bool usedAttack = false;
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        playerSpeed = 7;
        shakeOnLand = false;
        initialJump = true;
        pastFirstFrame = false;
        usedAttack = false;
        player.playerData.fastFallCounter = 0;
        yFallStart = player.transform.position.y;
        if (player.playerData.PlayerRb.linearVelocityY > 0) 
        {
            player.StartCoroutine(player.WaitUntilNotJumping());
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", true);
            changeYStartNextFall = true;
        }
        if (player.playerData.PlayerRb.linearVelocityY < 0) 
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
            player.playerData.anim.SetBool("superJumping",false);
        }
        
        if(player.playerData.jumpBufferCounter < -5)
        {
            player.playerData.coyoteTimeCounter = 15;
        }
    }
    public override void UpdateState(PlayerStateManager player)
    {
        player.playerData.fastFallCounter -= 1;

        // Exit Super Jump
        if (SettingsData.Instance._DoubleTapFastFall && UserInput.Instance.KeyDownCrouch)
        {
            player.playerData.fastFallCounter = 45;
        }
        if (UserInput.Instance.KeyDownCrouch)
        {
            player.playerData.resetVelocity = true;
            player.playerData.fastFallCounter = 0;
            player.SwitchState(player.AirState);
        }
        
        // Movement left/right.
        moving = false;
        if (!initialJump)
        {
            if (UserInput.Instance.MovementInput.x > 0.25f) // Moving Right
            {
                
                PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
                player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
                moving = true;
                player.playerData.resetVelocity = true;
            }
            if (UserInput.Instance.MovementInput.x < -0.25f) // Moving left
            {
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
        }
        if (player.playerData.PlayerRb.linearVelocityY < 0)
        {
            initialJump = false;
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
            player.playerData.anim.SetBool("superJumping",false);
            player.playerData.inAirGust = false;
            if (changeYStartNextFall)
            {
                yFallStart = player.transform.position.y;
                changeYStartNextFall = false;
            }
        }

        // Attacking
        if (player.playerData.bufferedAtk > 0 && !player.playerData.anim.GetBool("attacking") && !usedAttack)
        {
            player.StartCoroutine(DelayedBoost(player));
            player.Attack(PlayerStateManager.AttackType.jumpAttack,false);
            player.playerData.shortJumping = false;
            usedAttack = true;
        }

        // Short Jumping
        // If not attacking, enable short jumping.
        if (!player.playerData.anim.GetBool("attacking"))
        {
            player.playerData.shortJumping = true;
        }
        if(!(UserInput.Instance.KeyHeldDownJump || SettingsData.Instance._UpToJump && UserInput.Instance.MovementInput.y > 0.5f) && player.playerData.PlayerRb.linearVelocity.y > 0 && !player.playerData.inAirGust && player.isJumping && player.playerData.shortJumping)
        {
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, player.playerData.PlayerRb.linearVelocityY * 0.9f);
        }

        // Heavy Fall
        float fallDistance = yFallStart - player.transform.position.y;
        if(fallDistance > 12.5)
        {
            shakeOnLand = true;
            shakeIntensityLvl = fallDistance/1.6f + Mathf.Abs(player.playerData.PlayerRb.linearVelocityY)/3.5f;
        }
        else
        {
            shakeOnLand = false;
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
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            player.StartCoroutine(player.WaitUntilNotJumping());
            player.playerData.audioSource.PlayJumpSound(player._AirJump);
            player.playerData.anim.SetBool("jumping", true);
            player.playerData.doubleJumpAvailable = false;
            changeYStartNextFall = true;
            player.playerData.coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        // Grounded Check
        if (GroundCheck.Instance._IsGrounded && pastFirstFrame)
        {
            if(shakeOnLand)
            {
                TriggerShake.Instance.BurstShake(shakeIntensityLvl,1,true,0);
            }
            player.playerData.doubleJumpAvailable = true;
            player.playerData.audioSource.PlayJumpSound(player._NormalFall);
            if (GroundCheck.Instance._IsStone)
            {
                player.playerData.audioSource.PlayStoneSound(player._StoneFall);
            }
            else
            {
                player.playerData.audioSource.PlayGrassSound(player._GrassFall);
            }
            if((player.playerData.anim.GetInteger("attackId") == 2 || player.playerData.anim.GetInteger("attackId") == 4) && player.playerData.anim.GetBool("attacking"))
            {
                player.playerData.anim.SetBool("attacking", false);
            }
            player.playerData.fastFallCounter = 0;
            changeYStartNextFall = true;
            player.SwitchState(player.IdleState);
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", false);
            player.playerData.anim.SetBool("superJumping",false);
            return;
        }
        pastFirstFrame = true;
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
        player.playerData.anim.SetBool("currentlyFixed",false);
        player.playerData.anim.SetBool("jumping", false);
        player.playerData.anim.SetBool("falling", false);
        player.playerData.jumpBufferCounter = 0;
    }
    public IEnumerator DelayedBoost(PlayerStateManager player)
    {
        int elapsed = 0;
        while (elapsed < 8)
        {
            elapsed += Time.timeScale == 1 ? 1:0;
            yield return null;
        }
        player.playerData.PlayerRb.linearVelocity = new Vector2(4 * (player.playerData.leftOrRight? 1 : -1),(player.playerData.PlayerRb.linearVelocityY > 0 ? player.playerData.PlayerRb.linearVelocityY : 0) +jumpStrength);
        if(player.playerData.PlayerRb.linearVelocityY > 22.2f)
        {
            player.playerData.PlayerRb.linearVelocityY = 22.2f;
        }
        player.playerData.audioSource.PlayPlayerAttackSound(player._PlayerSpinAttack);
        changeYStartNextFall = true;
    }
}
