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
    }
    public override void UpdateState(PlayerStateManager player)
    {
        player.playerData.pickUpHeart = false;
        currentAttack = PlayerStateManager.AttackType.forward; // Default attack if nothing is inputed this frame.
        
        player.playerData.PlayerRb.linearVelocity = new Vector2(0,player.playerData.PlayerRb.linearVelocityY) + player.playerData.OffsetVelocity;
        

        // Check for Up Attack
        if (Input.GetKey(SettingsData.Instance._InputUp))
        {
            currentAttack = PlayerStateManager.AttackType.up;
        }

        // Attack
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            player.Attack(currentAttack);
        }

        // Grounded
        if (!GroundCheck.Instance._IsGrounded)
        {
            player.SwitchState(player.AirState);
            return;
        }

        // Movement
        if (Input.GetKey(SettingsData.Instance._InputLeft) || Input.GetKey(SettingsData.Instance._InputRight))
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

        // Jumping
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from idle");
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

}
