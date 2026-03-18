using UnityEngine;

public abstract class PlayerAbstract
{
    // States
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    //public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);
    public Vector2 PlayerVelocity;
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public GameObject Player;
    public float playerSpeed = 10f;
}