using UnityEngine;

public class PlayerDead : PlayerAbstract
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void RunOnce(PlayerStateManager player)
    {
    }
    public override void EnterState(PlayerStateManager player)
    {
        PlayerStateManager.Instance.playerData.playerHealth = 0;
    }
    public override void UpdateState(PlayerStateManager player)
    {
        PlayerStateManager.Instance.playerData.anim.SetBool("dead", true);
        if (PlayerStateManager.Instance.playerData.playerHealth <= 0) 
        {
            Debug.Log("You Are Dead");
            player.playerData.PlayerRb.linearVelocity = new Vector2(0, 0);
            PlayerStateManager.Instance.playerData.playerHealth = 0;
            PlayerStateManager.Instance.playerData.movementAllowed = false;
        }
    }
}
