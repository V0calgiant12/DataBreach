using UnityEngine;

public abstract class PlayerAbstract
{
    // States
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    //public abstract void OnCollisionEnter(PlayerStateManager player, Collision collision);
    public abstract void RunOnce(PlayerStateManager player);

    public Vector2 PlayerVelocity;
    public Vector2 OffsetVelocity;
    public Rigidbody2D PlayerRb;
    public GameObject Player;
    public RaycastHit2D groundHit;
    public float playerSpeed = 12f;
    public float jumpStrength = 15f;
    public int jumpBufferCounter;
    public int coyoteTimeCounter;
    public float raycastDistance;
    public bool fakeSprintToggle;
    public bool sprinting;
    public bool fakeCrouchToggle;
    public bool moving;
    public bool doubleJumpAvailable;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    public void Setup() 
    {
        fakeCrouchToggle = false;
        fakeSprintToggle = true;
        sprinting = false;
    }
    public void FindPlayerObject()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
    }
}