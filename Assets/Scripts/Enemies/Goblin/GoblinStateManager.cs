using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class GoblinStateManager : MonoBehaviour
{
    [Header("Movement & Gravity")]
    public float moveSpeed = 3f;
    public float patrolRange = 5f;
    public float chaseRange = 7f;
    public float jumpForce = 16f;
    public float forwardForce = 2f;
    public bool goblinLeftOrRight;


    [Header("Combat")]
    public float attackRange = 1.5f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;


    [Header("References")]
    public Transform player;

    private Rigidbody2D goblinRb;
    private Vector2 startPosition;
    private float patrolTargetX;
    private SpriteRenderer spriteRenderer;
    public EnemyGroundCheck EnemyGroundCheck;
    public EnemyWallTrigger EnemyWallTrigger;
    public bool isGrounded;
    public bool wallCollision;
    public bool isStone;
    public GameObject groundTrigger;
    public GameObject wallTrigger;
    public float playerTooHigh = 20.0f;

    void Start()
    {
        goblinRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        SetNewPatrolTarget();
        EnemyGroundCheck = groundTrigger.GetComponent<EnemyGroundCheck>();
        EnemyWallTrigger = wallTrigger.GetComponent<EnemyWallTrigger>();

        // Ensure the goblin doesn't tip over like a domino
        goblinRb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    void FixedUpdate()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        

        if (distanceToPlayer <= attackRange)
        {
            StopMovement();
            TryAttack();
        }
        else if (distanceToPlayer <= chaseRange)
        {
            Move(player.position.x > transform.position.x ? 1 : -1);
            if (player.position.y >= transform.position.y + 2)
            {
                if (isGrounded == true)
                {
                    Jump();
                    Debug.Log("Goblin Jumps");
                }
            }
        }
        else
        {
            Patrol();
        }
    }


    void Patrol()
    {
        float direction = patrolTargetX > transform.position.x ? 1 : -1;
        Move(direction);


        if (Mathf.Abs(transform.position.x - patrolTargetX) < 0.5f)
        {
            SetNewPatrolTarget();
        }
    }


    void Move(float direction)
    {
        // We only change the X velocity. Gravity handles the Y velocity.
        goblinRb.linearVelocity = new Vector2(direction * moveSpeed, goblinRb.linearVelocity.y);

        // Flip sprite
        spriteRenderer.flipX = direction < 0;

        //if (_IsGrounded )
    }


    void StopMovement()
    {
        goblinRb.linearVelocity = new Vector2(0, goblinRb.linearVelocity.y);
    }


    void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("Goblin Swings!");
            // ADD: animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackRate;
        }
    }


    void SetNewPatrolTarget()
    {
        patrolTargetX = startPosition.x + Random.Range(-patrolRange, patrolRange);
    }

    public void Jump()
    {
        float direction = (player.position.x > transform.position.x) ? 1f : -1f;

        if (direction == 1f)
        {
            goblinLeftOrRight = true;
        }
        if (direction == -1f)
        {
            goblinLeftOrRight = false;
        }

        Vector2 jumpVector = new Vector2(direction * forwardForce, jumpForce);
        goblinRb.AddForce(jumpVector, ForceMode2D.Impulse);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone"))
        {
            isGrounded = true;
        }
        if (other.gameObject.CompareTag("Stone"))
        {
            isStone = true;
        }
        else
        {
            isStone = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone"))
        {
            isGrounded = false;
            if (PlayerStateManager.Instance.playerData.jumpBufferCounter < 0)
            {
                PlayerStateManager.Instance.playerData.coyoteTimeCounter = 15;
            }
        }
        if (other.gameObject.CompareTag("Stone") && PlayerStateManager.Instance.playerData.coyoteTimeCounter < 0)
        {
            isStone = false;
        }
    }
}


