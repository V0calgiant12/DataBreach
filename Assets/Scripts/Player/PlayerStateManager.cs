using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerAbstract currentState;
    public PlayerAir AirState = new PlayerAir();
    public PlayerCrouching CrouchingState = new PlayerCrouching();
    public PlayerIdle IdleState = new PlayerIdle();
    public PlayerSprinting SprintingState = new PlayerSprinting();
    public PlayerWalking WalkingState = new PlayerWalking();
    //11:42 https://www.youtube.com/watch?v=Vt8aZDPzRjI
    public static PlayerStateManager Instance;

    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Awake()
    {
        currentState = IdleState;
        currentState.RunOnce(this);
        currentState.EnterState(this);

        // Move this to somewhere else later after merge.
	    QualitySettings.vSyncCount = 1;
	    Application.targetFrameRate = 60;
    }
    void Update()
    {
        currentState.UpdateState(this);
        FindPlayerObject();
    }
    public void SwitchState(PlayerAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }
    
    public void FindPlayerObject()
    {
        currentState.PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }
}