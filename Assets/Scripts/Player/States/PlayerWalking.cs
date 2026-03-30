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
        playerSpeed = 8;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        currentAttack  = PlayerStateManager.AttackType.forward; // Default to forward attack if nothing is inputed this frame.
        // Check for Up Attack
        if (Input.GetKey(SettingsData.Instance._InputDown))
        {
            currentAttack = PlayerStateManager.AttackType.upAir;
        }
        
        // Moving
        moving = false;
        player.playerData.anim.SetBool("attacking", false);
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = true;
            player.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            currentAttack = PlayerStateManager.AttackType.forward;
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = false;
            player.playerData.anim.SetBool("moving", true);
            moving = true;
        }

        // Attack
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            Debug.Log("Attacking while walking");
            player.playerData.anim.SetBool("attacking", true);
            player.Attack(60, currentAttack);
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
            player.SwitchState(player.IdleState);
            return;
        }

        // Jump
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from walking");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength);
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

        // Grounded
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            player.playerData.audioSource.PlayGrassSound(player.playerData._GrassJump);
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.AirState);
            return;
        }

        // Sprinting
        if (player.playerData.sprinting)
        {
            player.SwitchState(player.SprintingState);
            player.playerData.anim.SetBool("moving", true);
            player.playerData.anim.SetBool("sprinting", true);
            return;
        }

        // Audio
        if(audioTimer == 25)
        {
            if (GroundCheck.Instance._IsStone)
            {
                player.playerData.audioSource.PlayStoneSound(player.playerData._StoneWalk);
            }
            else
            {
                player.playerData.audioSource.PlayGrassSound(player.playerData._GrassWalk);
            }
            audioTimer = 0;
        }else
        {
            audioTimer += 1;
        }
    }
}
