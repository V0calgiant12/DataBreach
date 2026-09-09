using UnityEngine;
using System.Collections;
using NUnit.Framework.Internal.Filters;
public class PlayerSuperJump : PlayerAbstract
{
    private PlayerStateManager.AttackType currentAttack;
    private bool changeYStartNextFall = true;
    private bool pastFirstFrame = false;
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        playerSpeed = 7;
        shakeOnLand = false;
        pastFirstFrame = false;
        player.playerData.fastFallCounter = 0;
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
        Debug.Log("SuperJump");
    }
    public override void UpdateState(PlayerStateManager player)
    {
        player.playerData.fastFallCounter -= 1;

        // Exit Super Jump
        if (UserInput.Instance.KeyDownCrouch)
        {
            player.playerData.resetVelocity = true;
            if (SettingsData.Instance._DoubleTapFastFall && player.playerData.fastFallCounter > 0)
            {
                player.playerData.fastFallCounter = 0;
                player.SwitchState(player.AirState);
            }
            else if (!SettingsData.Instance._DoubleTapFastFall)
            {
                player.SwitchState(player.AirState);
            }
        }
        if (SettingsData.Instance._DoubleTapFastFall && UserInput.Instance.KeyDownCrouch)
        {
            player.playerData.fastFallCounter = 45;
        }
        
        
        if (player.playerData.PlayerRb.linearVelocityY < 0)
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
            player.playerData.anim.SetBool("superJumping",false);
            player.playerData.inAirGust = false;
            player.SwitchState(player.AirState);
        }

        // Attacking
        if (player.playerData.bufferedAtk > 0)
        {
            player.Attack(PlayerStateManager.AttackType.jumpAttack);
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
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * 0.8f);
            player.StartCoroutine(player.WaitUntilNotJumping());
            player.playerData.audioSource.PlayJumpSound(player.playerData._AirJump);
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
}
