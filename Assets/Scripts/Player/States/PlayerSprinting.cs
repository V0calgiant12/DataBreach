using UnityEngine;

public class PlayerSprinting : PlayerAbstract
{
    private int audioTimer = 0;
    public override void RunOnce(PlayerStateManager player)
    {
        Setup();
    }
    public override void EnterState(PlayerStateManager player)
    {
        //Debug.Log("Player is Sprinting / Sprinting State - " + player.playerData.sprinting);
        audioTimer = 3;
        player.playerData.anim.SetBool(player.sprinting, true);
        player.playerData.anim.SetBool(player.walking, false);
        player.playerData.anim.SetBool(player.moving, true);
        player.playerData.shortJumping = true;
        player.playerData.resetVelocity = true;
        player.playerData.playerSpeed = 15;
        player.playerData.basePlayerSpeed = 15;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if(player.playerData.playerSpeed != player.playerData.basePlayerSpeed && player.playerData.autoResetSpeed)
        {
            player.playerData.playerSpeed += player.playerData.playerSpeed > player.playerData.basePlayerSpeed ? -0.1f:0.1f;
            if(!player.playerData.resetVelocity)
            {
                player.playerData.PlayerRb.linearVelocityX -= player.playerData.leftOrRight? 0.1f : -0.1f;
            }
        }
        moving = false;

        // sprint right
        if (UserInput.Instance.MovementInput.x > 0.25f && player.playerData.movementAllowed)
        {
            PlayerVelocity = new Vector2(player.playerData.playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            moving = true;
        }
        // sprint left
        if (UserInput.Instance.MovementInput.x < -0.25f && player.playerData.movementAllowed)
        {
            PlayerVelocity = new Vector2(-player.playerData.playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            moving = true;
        }

        // Attacking
        if (player.playerData.bufferedAtk > 0 && player.dashAttackCd < 0)
        {
            player.Attack(PlayerStateManager.AttackType.dash,true);
        }

        // if crouching go to crouching
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            player.currentState.UpdateState(player);
            return;
        }
        // if not moving then go to idle
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
            player.currentState.UpdateState(player);
            return;
        }
        // if not sprinting go to walking 
        if (player.playerData.sprinting == false)
        {
            player.playerData.anim.SetBool(player.sprinting, false);
            //Debug.Log(player.playerData.sprinting);
            player.SwitchState(player.WalkingState);
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
                player.playerData.PlayerRb.linearVelocityX = 0;
                player.SwitchState(player.AirState);
                player.currentState.UpdateState(player);
                return;
            }
        }

        // Audio
        if(audioTimer == 9)
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
            player.playerData.anim.SetBool(player.sprinting, false);
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.audioSource.PlaySound(player._NormalFall,1,Random.Range(0.7f,1.4f),0,1,player.transform.position);
            if (GroundCheck.Instance._IsStone)
            {
                player.audioSource.PlaySound(player._StoneFall,1,Random.Range(0.6f,1.3f),0,1,player.transform.position);
            }
            else if(GroundCheck.Instance._IsWood)
            {
                player.audioSource.PlaySound(player._WoodLand,1f,Random.Range(0.7f,1.4f),0,1,player.transform.position);
            }
            else
            {
                player.audioSource.PlaySound(player._GrassFall,0.8f,Random.Range(0.8f,1.5f),0,1,player.transform.position);
            }
            if (!CheckGroundInFront(player))
            {
                player.forceSuperJump = false;
                player.SwitchState(player.DashingState);
            }
            else
            {
                player.SwitchState(player.AirState);
            }
            player.currentState.UpdateState(player);
            return;
        }
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
        player.playerData.sprintBufferCounter = 15;
    }
    private bool CheckGroundInFront(PlayerStateManager player)
    {
        RaycastHit2D forward = Physics2D.Raycast(new Vector2(player.transform.position.x,player.transform.position.y - 0.5f),player.playerData.leftOrRight ? Vector2.right:Vector2.left,1.5f,LayerMask.GetMask("Ground"));
        Debug.DrawRay(new Vector2(player.transform.position.x,player.transform.position.y - 0.5f),(player.playerData.leftOrRight ? Vector2.right:Vector2.left)*1.5f,Color.red);
        if (forward)
        {
            return forward;
        }
        RaycastHit2D down = Physics2D.Raycast(new Vector2(player.transform.position.x + (player.playerData.leftOrRight ? 1.5f:-1.5f),player.transform.position.y - 0.5f),Vector2.down,1,LayerMask.GetMask("Ground"));
        Debug.DrawRay(new Vector2(player.transform.position.x + (player.playerData.leftOrRight ? 1.5f:-1.5f),player.transform.position.y - 0.5f),Vector2.down*1f,Color.green);
        return down;
    }
}
