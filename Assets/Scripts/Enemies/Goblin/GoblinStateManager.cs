using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class GoblinStateManager : MonoBehaviour
{
    [Header("States")]
    public GoblinAbstract currentState;
    public GoblinAir AirState = new GoblinAir();
    public GoblinUpdate GlobalUpdateState = new GoblinUpdate();
    public GoblinIdle IdleState = new GoblinIdle();
    public GoblinPatrolling PatrollingState = new GoblinPatrolling();
    public GoblinChasing ChasingState = new GoblinChasing();

    [Header("Movement & Gravity")]
    public float moveSpeed = 3f;
    public float patrolRange = 5f;
    public float chaseRange = 7f;
    public float jumpForce = 16f;
    public float forwardForce = 2f;
    public bool leftOrRight;


    [Header("Combat")]
    public bool playerInRange = false;
    public float attackRate = 1.5f;


    [Header("References")]
    public Rigidbody2D goblinRb;
    public Vector2 originPos;
    public float patrolTargetX;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public EnemyGroundCheck EnemyGroundCheck;

    void Start()
    {
        goblinRb = GetComponent<Rigidbody2D>();
        originPos = transform.position;
        
        
        currentState = IdleState;
        currentState.RunOnce(this);
        GlobalUpdateState.EnterState(this);
        currentState.EnterState(this);
    }
    
    public void SwitchState(GoblinAbstract state)
    {
        currentState = state;
        state.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
    }


    public void Jump()
    {
        leftOrRight = (PlayerStateManager.Instance.transform.position.x > transform.position.x) ? true : false;

        goblinRb.linearVelocity = new Vector2((leftOrRight == true ? -1 : 1) * forwardForce, jumpForce);
        //goblinRb.linearVelocityY = 50;
    }
    public void WallCollision()
    {
        Debug.Log("Wall Collision");
        Jump();
    }
}


