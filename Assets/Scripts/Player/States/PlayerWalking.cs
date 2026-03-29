using UnityEngine;

public class PlayerWalking : PlayerAbstract
{
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
        moving = false;
        if (Input.GetKey(SettingsData.Instance._InputRight))
        {
            PlayerVelocity = new Vector2(playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = true;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (Input.GetKey(SettingsData.Instance._InputLeft)) 
        {
            PlayerVelocity = new Vector2(-playerSpeed, player.playerData.PlayerRb.linearVelocityY);
            player.playerData.PlayerRb.linearVelocity = PlayerVelocity;// + OffsetVelocity;
            player.playerData.leftOrRight = false;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
            moving = true;
        }
        if (player.playerData.crouching)
        {
            player.SwitchState(player.CrouchingState);
            return;
        }
        if (!moving)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", false);
            player.SwitchState(player.IdleState);
            return;
        }
        if (player.playerData.jumpBufferCounter > 0)
        {
            Debug.Log("jump from walking");
            player.playerData.PlayerRb.linearVelocity = new Vector2(player.playerData.PlayerRb.linearVelocityX, jumpStrength);
            player.playerData.jumpBufferCounter = 0;
            player.playerData.coyoteTimeCounter = 0;
            player.SwitchState(player.AirState);
            return;
        }
        if (!GroundCheck.Instance._IsGrounded && player.playerData.coyoteTimeCounter < 0)
        {
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.SwitchState(player.AirState);
            return;
        }
        if (player.playerData.sprinting)
        {
            player.SwitchState(player.SprintingState);
            PlayerStateManager.Instance.playerData.anim.SetBool("moving", true);
            PlayerStateManager.Instance.playerData.anim.SetBool("sprinting", true);
            return;
        }
        if(audioTimer == 10)
        {
            player.playerData.audioSource.PlayGrassSound(player.playerData._GrassWalk);
            audioTimer = 0;
        }else
        {
            audioTimer += 1;
        }
    }
}
