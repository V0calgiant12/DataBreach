using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GoblinStateManager : MonoBehaviour
{
    [Header("Movement & Gravity")]
    public float moveSpeed = 3f;
    public float patrolRange = 5f;
    public float chaseRange = 7f;
    
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

    void Start()
    {
        goblinRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        SetNewPatrolTarget();
        
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}