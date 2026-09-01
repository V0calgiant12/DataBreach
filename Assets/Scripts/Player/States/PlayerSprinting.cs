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
        player.playerData.anim.SetBool("sprinting", true);
        player.playerData.anim.SetBool("walking", false);
        player.playerData.anim.SetBool("moving", true);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        playerSpeed = 15f * PlayerStateManager.Instance.playerData.mudSpeedMulti;
        moving = false;

        // sprint right
        if (UserInput.Instance.MovementInput.x > 0.25f && player.playerData.movementAllowed)
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            moving = true;
            player.playerData.resetVelocity = true;
        }
        // sprint left
        if (UserInput.Instance.MovementInput.x < -0.25f && player.playerData.movementAllowed)
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            moving = true;
            player.playerData.resetVelocity = true;
        }

        // Attacking
        if (player.playerData.bufferedAtk > 0)
        {
            player.Attack(PlayerStateManager.AttackType.dash);
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
            player.playerData.anim.SetBool("sprinting", false);
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
            Debug.Log("jump from Sprinting");
            player.playerData.anim.SetBool("sprinting", false);
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
            if (!CheckGroundInFront(player))
            {
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
