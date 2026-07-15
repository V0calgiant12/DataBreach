using Unity.VisualScripting;
using System;
using System.Collections;
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
    public bool touchingWall = false;


    [Header("Combat")]
    public bool playerInRange = false;
    public float attackRate = 1.5f;


    [Header("References")]
    public Rigidbody2D goblinRb;
    public BoxCollider2D wallTrigger;
    public Vector2 originPos;
    public float patrolTargetX;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public EnemyGroundCheck groundCheck;

    void Start()
    {
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


    public IEnumerator Jump()
    {
        if (groundCheck._IsGrounded)
        {
            leftOrRight = (PlayerStateManager.Instance.transform.position.x > transform.position.x) ? true : false;

            goblinRb.linearVelocity = new Vector2((leftOrRight == true ? -1 : 1) * forwardForce, jumpForce);
            int elapsed = 0;
            while(elapsed != 5)
            {
                elapsed += Time.timeScale == 1 ? 1 : 0;
                if(elapsed == 5)
                {
                    StartCoroutine(ShortJump());
                }
                yield return null;
            }
        }
    }
    public IEnumerator ShortJump()
    {
        int elapsed = 0;
        while(!groundCheck._IsGrounded)
        {
            if (!touchingWall && goblinRb.linearVelocityY > 0)
            {
                elapsed += Time.timeScale == 1 ? 1 : 0;
                if(elapsed > 5)
                {
                    goblinRb.linearVelocity = new Vector2(goblinRb.linearVelocityX, goblinRb.linearVelocityY * 0.5f);
                }
            }
            yield return null;
        }
    }
    public void WallCollision()
    {
        Debug.Log("Wall Collision");
        touchingWall = true;
        StartCoroutine(Jump());
    }
}


