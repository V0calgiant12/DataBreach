using UnityEngine;

public class PlayerDead : PlayerAbstract
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player)
    {
    }
    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log(player.playerData.playerHealth);
        if (player.playerData.playerHealth <= 0) 
        {
            player.playerData.playerDead = true;
            player.playerData.anim.SetBool("dead", true);
            player.playerData.anim.SetBool("moving", false);
            player.playerData.anim.SetBool("sprinting", false);
            player.playerData.anim.SetBool("jumping", false);
            player.playerData.anim.SetBool("falling", false);
            player.playerData.anim.SetBool("crouching", false);
            player.playerData.PlayerRb.linearVelocityX = 0;
            player.playerData.audioSource.PlayPlayerDeathSound(player.playerData._PlayerDeath);
            player.playerData.DeathTransitionImage.SetActive(true);
            Debug.Log("You Are Dead");
            player.playerData.movementAllowed = false;
        }
        if (player.playerData.playerHealth >= 1)
        {
            player.playerData.playerDead = false;
            player.playerData.playerHealth = 1;
            Debug.Log(player.playerData.playerHealth);
            player.playerData.movementAllowed = true;
            player.SwitchState(player.IdleState);
        }
    }
}