using UnityEngine;


public class GlibberknockerStateManager : MonoBehaviour
{
    [Header("Movement & Gravity")]
    public float moveSpeed = 4f;
    public float patrolRange = 10f;
    public float chaseRange = 20f;

    [Header("Combat")]
    public float attackRange = 4f;
    public float attackRate = 0.5f;
    private float nextAttackTime = 0f;


    [Header("References")]
    public Transform player;

    private Rigidbody2D glibberknockerRb;
    private Vector2 startPosition;
    private float patrolTargetX;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        glibberknockerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        SetNewPatrolTarget();

        // Ensure the glibberknocker doesn't tip over like a domino
        glibberknockerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
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
        glibberknockerRb.linearVelocity = new Vector2(direction * moveSpeed, glibberknockerRb.linearVelocity.y);

        // Flip sprite
        spriteRenderer.flipX = direction < 0;
    }


    void StopMovement()
    {
        glibberknockerRb.linearVelocity = new Vector2(0, glibberknockerRb.linearVelocity.y);
    }


    void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Debug.Log("glibberknocker Swings!");
            // ADD: animator.SetTrigger("Attack");
            nextAttackTime = Time.time + attackRate;
        }
    }


    void SetNewPatrolTarget()
    {
        patrolTargetX = startPosition.x + Random.Range(-patrolRange, patrolRange);
    }



}


