using System.Collections;
using UnityEngine;

public class PlayerInteracting : PlayerAbstract
{
    private int frame = 0;
    public PlayerStateManager.InteractControls state;
    public float distance = 0;
    private float currentDistance;
    public float startPoint = 0;
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player) // Start Function
    {
        player.playerData.interactingCooldown = 30;
        player.playerData.interacting = true;
        frame = 0;
        startPoint = player.transform.position.x;
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        switch (state)
        {
            case(PlayerStateManager.InteractControls.Stop):
                player.playerData.PlayerRb.linearVelocityX = 0;
                player.playerData.anim.SetBool("moving", false);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                break;
            case(PlayerStateManager.InteractControls.WalkLeft):
                playerSpeed = 8*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -playerSpeed;
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", true);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.WalkRight):
                playerSpeed = 8*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = playerSpeed;
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", true);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.SprintLeft):
                playerSpeed = 15*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -playerSpeed;
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", true);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.SprintRight):
                playerSpeed = 15*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = playerSpeed;
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", true);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.FullJump):
                if (GroundCheck.Instance._IsGrounded)
                {
                    player.playerData.PlayerRb.linearVelocityY = jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti;
                    player.playerData.audioSource.PlayJumpSound(player._NormalJump);
                    if (GroundCheck.Instance._IsStone)
                    {
                        player.playerData.audioSource.PlayStoneSound(player._StoneJump);
                    }
                    else
                    {
                        player.playerData.audioSource.PlayGrassSound(player._GrassJump);
                    }
                    player.playerData.anim.SetBool("jumping", true);
                }
                player.playerData.anim.SetBool("moving", false);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                state = PlayerStateManager.InteractControls.Stop;
                break;
            case(PlayerStateManager.InteractControls.FullJumpRight):
                playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = playerSpeed;
                if (GroundCheck.Instance._IsGrounded)
                {
                    player.playerData.PlayerRb.linearVelocityY = jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti;
                    player.playerData.audioSource.PlayJumpSound(player._NormalJump);
                    if (GroundCheck.Instance._IsStone)
                    {
                        player.playerData.audioSource.PlayStoneSound(player._StoneJump);
                    }
                    else
                    {
                        player.playerData.audioSource.PlayGrassSound(player._GrassJump);
                    }
                    player.playerData.anim.SetBool("jumping", true);
                }
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = Mathf.Abs(player.transform.position.x - startPoint);
                Debug.Log(currentDistance+ " " +distance+ " "+ (currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f));
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.FullJumpLeft):
                playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -playerSpeed;
                if (GroundCheck.Instance._IsGrounded)
                {
                    player.playerData.PlayerRb.linearVelocityY = jumpStrength * PlayerStateManager.Instance.playerData.mudJumpMulti;
                    player.playerData.audioSource.PlayJumpSound(player._NormalJump);
                    if (GroundCheck.Instance._IsStone)
                    {
                        player.playerData.audioSource.PlayStoneSound(player._StoneJump);
                    }
                    else
                    {
                        player.playerData.audioSource.PlayGrassSound(player._GrassJump);
                    }
                    player.playerData.anim.SetBool("jumping", true);
                }
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.ShortJump):
                if (GroundCheck.Instance._IsGrounded)
                {
                    player.playerData.PlayerRb.linearVelocityY = jumpStrength/2 * PlayerStateManager.Instance.playerData.mudJumpMulti;
                    player.playerData.audioSource.PlayJumpSound(player._NormalJump);
                    if (GroundCheck.Instance._IsStone)
                    {
                        player.playerData.audioSource.PlayStoneSound(player._StoneJump);
                    }
                    else
                    {
                        player.playerData.audioSource.PlayGrassSound(player._GrassJump);
                    }
                    player.playerData.anim.SetBool("jumping", true);
                }
                player.playerData.anim.SetBool("moving", false);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                state = PlayerStateManager.InteractControls.Stop;
                break;
            case(PlayerStateManager.InteractControls.ShortJumpRight):
                playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = playerSpeed;
                if (GroundCheck.Instance._IsGrounded)
                {
                    player.playerData.PlayerRb.linearVelocityY = jumpStrength/2 * PlayerStateManager.Instance.playerData.mudJumpMulti;
                    player.playerData.audioSource.PlayJumpSound(player._NormalJump);
                    if (GroundCheck.Instance._IsStone)
                    {
                        player.playerData.audioSource.PlayStoneSound(player._StoneJump);
                    }
                    else
                    {
                        player.playerData.audioSource.PlayGrassSound(player._GrassJump);
                    }
                    player.playerData.anim.SetBool("jumping", true);
                }
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = Mathf.Abs(player.transform.position.x - startPoint);
                Debug.Log(currentDistance+ " " +distance+ " "+ (currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f));
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.ShortJumpLeft):
                playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -playerSpeed;
                if (GroundCheck.Instance._IsGrounded)
                {
                    player.playerData.PlayerRb.linearVelocityY = jumpStrength/2 * PlayerStateManager.Instance.playerData.mudJumpMulti;
                    player.playerData.audioSource.PlayJumpSound(player._NormalJump);
                    if (GroundCheck.Instance._IsStone)
                    {
                        player.playerData.audioSource.PlayStoneSound(player._StoneJump);
                    }
                    else
                    {
                        player.playerData.audioSource.PlayGrassSound(player._GrassJump);
                    }
                    player.playerData.anim.SetBool("jumping", true);
                }
                player.playerData.anim.SetBool("moving", true);
                player.playerData.anim.SetBool("walking", false);
                player.playerData.anim.SetBool("sprinting", false);
                player.playerData.anim.SetBool("crouching", false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
        }
    }
    public override void LateUpdateState(PlayerStateManager player)
    {
        if(frame > 15)
        {
            if (UserInput.Instance.KeyDownInteract || UserInput.Instance.KeyDownAttack && TextWrite.Instance._Writing == false)
            {
                //player.playerData.interacting = false;
                //TextWrite.Instance.Close();
                //player.SwitchState(player.IdleState);
            }
        }
        else
        {
            frame += 1;
        }
        
        // Falling Animation
        if (player.playerData.PlayerRb.linearVelocityY < 0) 
        {
            player.playerData.anim.SetBool("falling", true);
            player.playerData.anim.SetBool("jumping", false);
        }
        if (GroundCheck.Instance._IsGrounded)
        {
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("jumping", false);
        }
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
    }
}
