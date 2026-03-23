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
    public Vector2 JumpBoxcastSize = new Vector2(1, 0.5f);
    public LayerMask Ground;
    public Rigidbody2D PlayerRb;
    public GameObject Player;
    public RaycastHit2D groundHit;
    public float playerSpeed = 10f;
    public float raycastDistance;
    public bool fakeSprintToggle;
    public bool sprinting;
    public bool fakeCrouchToggle;
    public bool moving;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    public void Setup() 
    {
        Ground = LayerMask.GetMask("Ground");
        fakeCrouchToggle = false;
        fakeSprintToggle = true;
        sprinting = false;
    }
    public void FindPlayerObject()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
    }
    public bool IsGrounded()
    {
        return groundHit = Physics2D.BoxCast(new Vector2(Player.transform.position.x, Player.transform.position.y - 1.5f), JumpBoxcastSize, 0f, Vector2.down, 3f, Ground);
        //Gizmos.DrawWireCube()
    }
}