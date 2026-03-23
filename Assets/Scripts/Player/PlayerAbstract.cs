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
    public Vector2 JumpBoxcastSize = new Vector2(1, 0.25f);
    public LayerMask Ground;
    public Rigidbody2D PlayerRb;
    public GameObject Player;
    public RaycastHit2D groundHit;
    public float playerSpeed = 10f;
    public float raycastDistance;
    public bool fakeSprintToggle;
    public bool sprintToggler;
    public bool fakeCrouchToggle;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    public void Setup() 
    {
        Ground = LayerMask.GetMask("Ground");
        Player = GameObject.Find("Player");
        PlayerRb = Player.GetComponent<Rigidbody2D>();
        fakeCrouchToggle = false;
        fakeSprintToggle = true;
        sprintToggler = false;
    }
    public bool IsGrounded()
    {
        Debug.Log("Checking for ground");
        return groundHit = Physics2D.BoxCast(new Vector2(Player.transform.position.x, Player.transform.position.y - 1), JumpBoxcastSize, 0f, Vector2.down, 0.10f, Ground);
    }
}