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
        player.playerData.anim.SetBool("dead", true);
        if (player.playerData.playerHealth <= 0) 
        {
            player.playerData.audioSource.PlayPlayerDeathSound(player.playerData._PlayerDeath);
            Debug.Log("You Are Dead");
            player.playerData.PlayerRb.linearVelocity = new Vector2(0, 0);
            player.playerData.playerHealth = 0;
            player.playerData.movementAllowed = false;
        }
    }
}
