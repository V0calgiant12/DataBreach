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
    public RaycastHit2D groundHit;
    public float playerSpeed = 12f;
    public float jumpStrength = 15f;
    public int jumpBufferCounter;
    public int coyoteTimeCounter;
    public int lastWallJumpRight;
    public float raycastDistance;
    public bool fakeSprintToggle;
    public bool sprinting;
    public bool fakeCrouchToggle;
    public bool moving;
    public bool doubleJumpAvailable;
    public bool currentWallSide;
    // video I used for this: https://www.youtube.com/watch?v=lbB64oWbhuc
    public void Setup() 
    {
        fakeCrouchToggle = false;
    }
    public void Update()
    {
        Debug.Log("Sprinting - " + sprinting);
        // Counter countdowns
        jumpBufferCounter -= 1;
        coyoteTimeCounter -= 1;

        // Set jump buffer if pressed
        if(Input.GetKeyDown(SettingsData.Instance._InputJump) || SettingsData.Instance._UpToJump && Input.GetKeyDown(SettingsData.Instance._InputUp))
        {
            Debug.Log("Jump");
            jumpBufferCounter = 5;
        }
        // Toggle sprint
        fakeSprintToggle = true;
        if (Input.GetKeyDown(SettingsData.Instance._InputSprint) && fakeSprintToggle)
        {
            Debug.Log("Toggle sprint " + fakeSprintToggle);
            sprinting = !sprinting;
        }
        // No toggle sprint
        if (fakeSprintToggle == false && Input.GetKeyDown(SettingsData.Instance._InputSprint))
        {
            Debug.Log("Holding Sprint " + fakeSprintToggle);
            sprinting = true;
        }
        else if (fakeSprintToggle == false && Input.GetKeyUp(SettingsData.Instance._InputSprint))
        {
            Debug.Log("Let go of sprint " + fakeSprintToggle);
            sprinting = false;
        }
    }
}