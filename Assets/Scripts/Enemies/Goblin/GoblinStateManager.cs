using Unity.VisualScripting;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class GoblinStateManager : MonoBehaviour
{
    [Header("States")]
    public GoblinAbstract currentState;
    public GoblinAttack AttackState = new GoblinAttack();
    public GoblinUpdate GlobalUpdateState = new GoblinUpdate();
    public GoblinIdle IdleState = new GoblinIdle();
    public GoblinPatrolling PatrollingState = new GoblinPatrolling();
    public GoblinChasing ChasingState = new GoblinChasing();
    public GoblinHurt HurtState = new GoblinHurt();
    public GoblinDead DeadState = new GoblinDead();

    [Header("Movement & Gravity")]
    public float moveSpeed = 3f;
    public float mudSpeedMulti = 1f;
    public float patrolRange = 5f;
    public float chaseRange = 7f;
    public float jumpForce = 16f;
    public float mudJumpMulti = 1f;
    public float forwardForce = 2f;
    public bool leftOrRight;
    public bool touchingWall = false;


    [Header("Combat")]
    public int attackCD = 60;
    public int currentAtkCd = 0;


    [Header("References")]
    public Rigidbody2D goblinRb;
    public BoxCollider2D wallTrigger;
    public Animator anim;
    public GameObject spriteHolder;
    public Vector2 originPos;
    public float patrolTargetX;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public EnemyGroundCheck groundCheck;
    public EnemyAttackRange attackRange;
    public EnemyHit enemyHit;

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
        if(enemyHit.trackedHealth != 0)
        {
            if(currentState != HurtState)
            {
                GlobalUpdateState.UpdateState(this);
            }
            if (groundCheck._IsGrounded)
            {
                anim.SetBool("falling", false);
            }
        }
        else
        {
            SwitchState(DeadState);
        }
    }


    public IEnumerator Jump()
    {
        if (groundCheck._IsGrounded && currentState != AttackState && currentState != DeadState)
        {
            leftOrRight = (PlayerStateManager.Instance.transform.position.x > transform.position.x) ? true : false;

            goblinRb.linearVelocity = new Vector2((leftOrRight == true ? -1 : 1) * forwardForce, jumpForce * mudJumpMulti);
            int elapsed = 0;
            anim.SetBool("jumping", true);
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
                    goblinRb.linearVelocity = new Vector2(goblinRb.linearVelocityX, goblinRb.linearVelocityY * 0.8f);
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


