using UnityEngine;

public class PlayerWalking : PlayerAbstract
{
    private PlayerStateManager.AttackType currentAttack;
    private int audioTimer = 0;
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Player is Walking / Walking State - " + player.playerData.sprinting);
        audioTimer = 0;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        playerSpeed = 8 * PlayerStateManager.Instance.playerData.mudSpeedMulti;
        currentAttack  = PlayerStateManager.AttackType.forward; // Default to forward attack if nothing is inputed this frame.
        
        // Moving
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight) && player.playerData.movementAllowed)
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            player.playerData.anim.SetBool("moving", true);
            player.playerData.anim.SetBool("walking", true);
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft) && player.playerData.movementAllowed) 
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            player.playerData.anim.SetBool("moving", true);
            player.playerData.anim.SetBool("walking", true);
            moving = true;
        }

        // Check for Up Attack
        if (Input.GetKey(SettingsData.Instance._InputUp))
        {
            currentAttack = PlayerStateManager.AttackType.up;
        }
        
        // Sprinting (placed in this weird spot because of a bug. Yes, I know, it looks ugly here now.)
        if (player.playerData.sprinting)
        {
            currentAttack = PlayerStateManager.AttackType.dash;
            player.SwitchState(player.SprintingState);
            player.playerData.anim.SetBool("moving", true);
            player.playerData.anim.SetBool("walking", false);
            player.playerData.anim.SetBool("sprinting", true);
            player.currentState.UpdateState(player);
            return;
        }

        // Attack
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            player.Attack(currentAttack);
        }

        // Crouch
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            return;
        }

        // Idle
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.playerData.anim.SetBool("moving", false);
            player.playerData.anim.SetBool("walking", false);
            player.SwitchState(player.IdleState);
            player.currentState.UpdateState(player);
            return;
        }

        // Jump
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from walking");
            player.playerData.anim.SetBool("walking", false);
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
            player.SwitchState(player.AirState);
            player.currentState.UpdateState(player);
            return;
        }

        // Grounded
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            player.playerData.audioSource.PlayGrassSound(player.playerData._GrassJump);
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.AirState);
            player.currentState.UpdateState(player);
            return;
        }

        // Audio
        if(audioTimer == 11)
        {
            if (!player.playerData.inMud)
            {
                if (GroundCheck.Instance._IsStone)
                {
                    player.playerData.audioSource.PlayStoneSound(player.playerData._StoneWalk);
                }
                else
                {
                    player.playerData.audioSource.PlayGrassSound(player.playerData._GrassWalk);
                }
            }
            else
            {
                player.playerData.audioSource.PlayMudSound(player.playerData._MudWalk[0]);
            }
            audioTimer = 0;
        }
        else
        {
            audioTimer += 1;
        }
    }
}
