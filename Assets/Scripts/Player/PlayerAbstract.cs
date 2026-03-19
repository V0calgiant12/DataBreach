using UnityEngine;

public abstract class PlayerAbstract
{
    // States
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    //public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);

    public Vector2 PlayerVelocity;
    public Vector2 OffsetVelocity;
    public Vector2 JumpRaycastSize;
    public LayerMask Ground;
    public Rigidbody2D PlayerRb;
    public GameObject Player;
    public float playerSpeed = 10f;
    public float raycastDistance;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    public bool IsGrounded()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Player.transform.position, Vector2.down, 1.0f, Ground);
        return hit;
    }
}