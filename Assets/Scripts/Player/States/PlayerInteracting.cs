using System.Collections;
using UnityEngine;

public class PlayerInteracting : PlayerAbstract
{
    private int frame = 0;
    private int audioTimer = 0;
    public PlayerStateManager.InteractControls state;
    public bool firstInteractFrame = true;
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
        firstInteractFrame = true;
    }
    public override void UpdateState(PlayerStateManager player) // Update Function
    {
        if (firstInteractFrame)
        {
            audioTimer = state == PlayerStateManager.InteractControls.SprintRight || state == PlayerStateManager.InteractControls.SprintLeft ? 3:0;
            startPoint = player.transform.position.x;
            firstInteractFrame = false;
        }
        switch (state)
        {
            case(PlayerStateManager.InteractControls.Stop):
                player.playerData.PlayerRb.linearVelocityX = 0;
                player.playerData.anim.SetBool(player.moving, false);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                break;
            case(PlayerStateManager.InteractControls.WalkLeft):
                player.playerData.playerSpeed = 8*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -player.playerData.playerSpeed;
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, true);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.WalkRight):
                player.playerData.playerSpeed = 8*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = player.playerData.playerSpeed;
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, true);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.SprintLeft):
                player.playerData.playerSpeed = 15*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -player.playerData.playerSpeed;
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, true);
                player.playerData.anim.SetBool(player.crouching, false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.SprintRight):
                player.playerData.playerSpeed = 15*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = player.playerData.playerSpeed;
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, true);
                player.playerData.anim.SetBool(player.crouching, false);
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
                    player.playerData.anim.SetBool(player.jumping, true);
                }
                player.playerData.anim.SetBool(player.moving, false);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                state = PlayerStateManager.InteractControls.Stop;
                break;
            case(PlayerStateManager.InteractControls.FullJumpRight):
                player.playerData.playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = player.playerData.playerSpeed;
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
                    player.playerData.anim.SetBool(player.jumping, true);
                }
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                currentDistance = Mathf.Abs(player.transform.position.x - startPoint);
                Debug.Log(currentDistance+ " " +distance+ " "+ (currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f));
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.FullJumpLeft):
                player.playerData.playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -player.playerData.playerSpeed;
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
                    player.playerData.anim.SetBool(player.jumping, true);
                }
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
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
                    player.playerData.anim.SetBool(player.jumping, true);
                }
                player.playerData.anim.SetBool(player.moving, false);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                state = PlayerStateManager.InteractControls.Stop;
                break;
            case(PlayerStateManager.InteractControls.ShortJumpRight):
                player.playerData.playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = true;
                player.playerData.PlayerRb.linearVelocityX = player.playerData.playerSpeed;
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
                    player.playerData.anim.SetBool(player.jumping, true);
                }
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                currentDistance = Mathf.Abs(player.transform.position.x - startPoint);
                Debug.Log(currentDistance+ " " +distance+ " "+ (currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f));
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
            case(PlayerStateManager.InteractControls.ShortJumpLeft):
                player.playerData.playerSpeed = 7*PlayerStateManager.Instance.playerData.mudSpeedMulti;
                player.playerData.leftOrRight = false;
                player.playerData.PlayerRb.linearVelocityX = -player.playerData.playerSpeed;
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
                    player.playerData.anim.SetBool(player.jumping, true);
                }
                player.playerData.anim.SetBool(player.moving, true);
                player.playerData.anim.SetBool(player.walking, false);
                player.playerData.anim.SetBool(player.sprinting, false);
                player.playerData.anim.SetBool(player.crouching, false);
                currentDistance = player.transform.position.x - startPoint;
                if(currentDistance <= distance+0.1f && currentDistance >= distance - 0.1f)
                {
                    state = PlayerStateManager.InteractControls.Stop;
                }
                break;
        }
        // Audio
        if((audioTimer == 11 && (state == PlayerStateManager.InteractControls.WalkLeft || state == PlayerStateManager.InteractControls.WalkRight)) || (audioTimer == 5 && (state == PlayerStateManager.InteractControls.SprintLeft || state == PlayerStateManager.InteractControls.SprintRight)))
        {
            if (!player.playerData.inMud)
            {
                if (GroundCheck.Instance._IsStone)
                {
                    player.playerData.audioSource.PlayStoneSound(player._StoneWalk);
                }
                else
                {
                    player.playerData.audioSource.PlayGrassSound(player._GrassWalk);
                }
            }
            else
            {
                player.playerData.audioSource.PlayMudSound(player._MudWalk[0]);
            }
            audioTimer = 0;
        }
        else
        {
            audioTimer += Time.timeScale == 1 ? 1:0;
        }
        
        player.playerData.basePlayerSpeed = player.playerData.playerSpeed;
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
            player.playerData.anim.SetBool(player.falling, true);
            player.playerData.anim.SetBool(player.jumping, false);
        }
        if (GroundCheck.Instance._IsGrounded)
        {
            player.playerData.anim.SetBool(player.falling, false);
            player.playerData.anim.SetBool(player.jumping, false);
        }
    }
    public override void LeaveState(PlayerStateManager player)
    {
        player.comingFromDash = false;
    }
}
