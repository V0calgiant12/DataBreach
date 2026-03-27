using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerUpdate GlobalUpdateState = new PlayerUpdate();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    //11:42 https://www.youtube.com/watch?v=Vt8aZDPzRjI
    public static PlayerStateManager Instance;
    public PlayerData playerData;
    void Start()
    {
        Instance = this;
        FindPlayerObject();
        currentState = IdleState;
        currentState.RunOnce(this);
        GlobalUpdateState.EnterState(this);
        currentState.EnterState(this);

        // Move this to somewhere else later after merge.
	    QualitySettings.vSyncCount = 1;
	    Application.targetFrameRate = 60;
    }
    void Update()
    {
        if (playerData.movementAllowed)
        {
            currentState.UpdateState(this);
        }
        GlobalUpdateState.UpdateState(this);
        FindPlayerObject();

        // Counter countdowns
        playerData.jumpBufferCounter -= 1;
        playerData.coyoteTimeCounter -= 1;

    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    public void FindPlayerObject()
    {
        playerData.PlayerRb = gameObject.GetComponent<Rigidbody2D>();
        playerData.collider = gameObject.GetComponent<BoxCollider2D>();
        playerData.MainCamera = GameObject.Find("Main Camera");
    }
}