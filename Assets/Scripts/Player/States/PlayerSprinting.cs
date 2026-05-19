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
        Debug.Log("Player is Sprinting / Sprinting State - " + player.playerData.sprinting);
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
        if (Input.GetKey(SettingsData.Instance._InputRight) && player.playerData.movementAllowed)
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = true;
            moving = true;
        }
        // sprint left
        if (Input.GetKey(SettingsData.Instance._InputLeft) && player.playerData.movementAllowed)
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity + player.playerData.OffsetVelocity;
            player.playerData.leftOrRight = false;
            moving = true;
        }

        // Attacking
        if (Input.GetKeyDown(SettingsData.Instance._InputAttack))
        {
            player.Attack(PlayerStateManager.AttackType.dash);
        }

        // if crouching go to crouching
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            return;
        }
        // if not moving then go to idle
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.IdleState);
            return;
        }
        // if not sprinting go to walking 
        if (player.playerData.sprinting == false)
        {
            player.playerData.anim.SetBool("sprinting", false);
            Debug.Log(player.playerData.sprinting);
            player.SwitchState(player.WalkingState);
            return;
        }

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
            player.SwitchState(player.AirState);
            return;
        }

        // Grounded
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            player.SwitchState(player.AirState);
            return;
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
}
