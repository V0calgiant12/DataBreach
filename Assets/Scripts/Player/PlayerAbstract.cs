using UnityEngine;

public abstract class PlayerAbstract
{
    // States
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);
}
