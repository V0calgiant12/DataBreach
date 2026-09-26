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
        player.playerData.shortJumping = true;
        player.playerData.resetVelocity = true;
        player.playerData.playerSpeed = 8;
        player.playerData.basePlayerSpeed = 8;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if(player.playerData.playerSpeed != player.playerData.basePlayerSpeed && player.playerData.autoResetSpeed)
        {
            player.playerData.playerSpeed += player.playerData.playerSpeed > player.playerData.basePlayerSpeed ? -0.05f:0.05f;
            if(!player.playerData.resetVelocity)
            {
                player.playerData.PlayerRb.linearVelocityX -= player.playerData.leftOrRight? 0.05f : -0.05f;
            }
        }
        currentAttack  = PlayerStateManager.AttackType.forward; // Default to forward attack if nothing is inputed this frame.
        
        // Moving
        moving = false;
        if (UserInput.Instance.MovementInput.x > 0.25f && player.playerData.movementAllowed)
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(player.playerData.playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            player.playerData.anim.SetBool(player.moving, true);
            player.playerData.anim.SetBool(player.walking, true);
            moving = true;
        }
        if (UserInput.Instance.MovementInput.x < -0.25f && player.playerData.movementAllowed) 
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(-player.playerData.playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            player.playerData.anim.SetBool(player.moving, true);
            player.playerData.anim.SetBool(player.walking, true);
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
            player.playerData.anim.SetBool(player.moving, true);
            player.playerData.anim.SetBool(player.walking, false);
            player.playerData.anim.SetBool(player.sprinting, true);
            player.currentState.UpdateState(player);
            return;
        }

        // Attacking
        if (player.playerData.bufferedAtk > 0) // Check for an attack.
        {
            // Button Press
            if(player.playerData.bufferedAtkDir == new Vector2(0, 0))
            {
                player.Attack(currentAttack,true);
            }
            // C-Stick Attacking
            if(player.playerData.bufferedAtkDir.y > 0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.up;
                player.Attack(currentAttack,true);
            }
            if(player.playerData.bufferedAtkDir.y < -0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.down;
                player.Attack(currentAttack,true);
            }
            if(player.playerData.bufferedAtkDir.x > 0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.forward;
                player.Attack(currentAttack,true);
            }
            if(player.playerData.bufferedAtkDir.x < -0.5f)
            {
                currentAttack = PlayerStateManager.AttackType.forward;
                player.Attack(currentAttack,true);
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
            player.playerData.anim.SetBool(player.moving, false);
            player.playerData.anim.SetBool(player.walking, false);
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
            if(!player.playerData.inMud)
            {
                if(GroundCheck.Instance._IsStone)
                {
                    player.audioSource.PlaySound(player._StoneWalk,1f,Random.Range(0.6f,1.3f),0,1,player.transform.position);
                }
                else if(GroundCheck.Instance._IsWood)
                {
                    player.audioSource.PlaySound(player._WoodWalk[Random.Range(0,6)],0.8f,Random.Range(0.6f,1.3f),0,1,player.transform.position);
                }
                else
                {
                    player.audioSource.PlaySound(player._GrassWalk,0.8f,Random.Range(0.8f,1.5f),0,1,player.transform.position);
                }
            }
            else
            {
                player.audioSource.PlaySound(player._MudWalk[0],1f,Random.Range(0.8f,1.2f),0,1,player.transform.position);
            }
            audioTimer = 0;
        }
        else
        {
            audioTimer += Time.timeScale == 1 ? 1:0;
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        // Jump
        if (player.playerData.jumpBufferCounter > 0)
        {
            //Debug.Log("jump from walking");
            player.playerData.anim.SetBool(player.walking, false);
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.audioSource.PlaySound(player._NormalJump,1,Random.Range(0.7f,1.4f),0,1,player.transform.position);
            if (GroundCheck.Instance._IsStone)
            {
                player.audioSource.PlaySound(player._StoneJump,1,Random.Range(0.6f,1.3f),0,1,player.transform.position);
            }
            else if(GroundCheck.Instance._IsWood)
            {
                player.audioSource.PlaySound(player._WoodJump,1,Random.Range(0.7f,1.4f),0,1,player.transform.position);
            }
            else
            {
                player.audioSource.PlaySound(player._GrassJump,0.8f,Random.Range(0.8f,1.5f),0,1,player.transform.position);
            }
            player.SwitchState(player.AirState);
            player.currentState.UpdateState(player);
            return;
        }
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
    }
}
