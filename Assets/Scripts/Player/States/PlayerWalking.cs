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
        //Debug.Log("Player is Walking / Walking State - " + player.playerData.sprinting);
        audioTimer = 0;
        player.playerData.resetVelocity = true;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        playerSpeed = 8 * PlayerStateManager.Instance.playerData.mudSpeedMulti;
        currentAttack  = PlayerStateManager.AttackType.forward; // Default to forward attack if nothing is inputed this frame.
        
        // Moving
        moving = false;
        if (UserInput.Instance.MovementInput.x > 0.25f && player.playerData.movementAllowed)
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            player.playerData.anim.SetBool("moving", true);
            player.playerData.anim.SetBool("walking", true);
            moving = true;
            player.playerData.resetVelocity = true;
        }
        if (UserInput.Instance.MovementInput.x < -0.25f && player.playerData.movementAllowed) 
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            player.playerData.anim.SetBool("moving", true);
            player.playerData.anim.SetBool("walking", true);
            moving = true;
            player.playerData.resetVelocity = true;
        }

        // Check for Up Attack
        if (UserInput.Instance.MovementInput.y > 0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.up;
        }
        else if (UserInput.Instance.MovementInput.y > 0 && UserInput.Instance.MovementInput.x > -0.5f && UserInput.Instance.MovementInput.x < 0.5f)
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
                currentAttack = PlayerStateManager.AttackType.up;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.y < -0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.down;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.x > 0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.forward;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.x < -0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.forward;
                player.Attack(currentAttack);
            }
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

        // Grounded
        if (!GroundCheck.Instance._IsGrounded)
        {
            if(player.playerData.coyoteTimeCounter < 0)
            {
                player.playerData.coyoteTimeCounter = 1;
            }
            if(player.playerData.coyoteTimeCounter == 0)
            {
                player.SwitchState(player.AirState);
                player.currentState.UpdateState(player);
                return;
            }
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
    public override void LateUpdateState(PlayerStateManager player)
    {
        // Jump
        if (player.playerData.jumpBufferCounter > 0)
        {
            //Debug.Log("jump from walking");
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
    }
}
