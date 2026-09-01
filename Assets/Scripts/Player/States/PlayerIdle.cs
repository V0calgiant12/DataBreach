using UnityEngine;

public class PlayerIdle : PlayerAbstract
{
    private PlayerStateManager.AttackType currentAttack;
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Player Idle / Idle State");
        player.playerData.anim.SetBool("moving", false);
        player.playerData.anim.SetBool("sprinting", false);
        player.playerData.resetVelocity = true;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        player.playerData.pickUpHeart = false;
        currentAttack = PlayerStateManager.AttackType.forward; // Default attack if nothing is inputed this frame.
        
        player.playerData.PlayerRb.linearVelocity = new Vector2(0,player.playerData.PlayerRb.linearVelocityY) + player.playerData.OffsetVelocity;
        

        // Check for Up Attack
        if (UserInput.Instance.MovementInput.y > 0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.up;
        }
        else if (UserInput.Instance.MovementInput.y > 0 && UserInput.Instance.MovementInput.x > -0.5f && UserInput.Instance.MovementInput.x < 0.5f)
        {
            currentAttack = PlayerStateManager.AttackType.up;
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
                player.playerData.leftOrRight = true;
                currentAttack = PlayerStateManager.AttackType.forward;
                player.Attack(currentAttack);
            }
            if(player.playerData.bufferedAtkDir.x < -0.5f)
            {
                player.playerData.leftOrRight = false;
                currentAttack = PlayerStateManager.AttackType.forward;
                player.Attack(currentAttack);
            }
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

        // Movement
        if (UserInput.Instance.MovementInput.x < -0.25f || UserInput.Instance.MovementInput.x > 0.25f)
        {
            player.SwitchState(player.WalkingState);
            player.playerData.anim.SetBool("moving", true);
            return;
        }

        // Crouching
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            return;
        }

    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        // Jumping
        if (player.playerData.jumpBufferCounter > 0)
        {
            //Debug.Log("jump from idle");
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
            return;
        }
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
    }
}
