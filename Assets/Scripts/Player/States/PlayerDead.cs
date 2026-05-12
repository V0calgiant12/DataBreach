using UnityEngine;

public class PlayerDead : PlayerAbstract
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player)
    {
        player.playerData.playerHealth = 0;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        Debug.Log(player.playerData.playerHealth);
        player.playerData.anim.SetBool("dead", true);
        if (player.playerData.playerHealth <= 0) 
        {
            player.playerData.playerDead = true;
            player.playerData.audioSource.PlayPlayerDeathSound(player.playerData._PlayerDeath);
            player.playerData.DeathTransition.SetActive(true);
            Debug.Log("You Are Dead");
            player.playerData.PlayerRb.linearVelocity = new Vector2(0, 0);
            player.playerData.playerHealth = 0;
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