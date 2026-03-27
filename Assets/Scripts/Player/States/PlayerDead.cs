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
        if (PlayerStateManager.Instance.playerData.playerHealth <= 0) 
        {
            PlayerStateManager.Instance.playerData.playerHealth = 0;
            PlayerStateManager.Instance.playerData.movementAllowed = false;
        }
    }
}
