using System.Collections;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class SlimeStateManager : EnemyAbstract
{
    public enum State { Idle, Chase, Dead}
    public State currentState = State.Idle;

    [Header("Jump Settings")]
    public float jumpForce = 5f;      // Upward power
    public float waterJumpMulti = 1f;
    public float forwardForce = 1f;   // Horizontal power toward player
    public float waterSpeedMulti = 1f;

    [Header("Detection")]
    public LayerMask groundLayer;
    public bool floating;

    [Header("Variables")]
    public bool slimeLeftOrRight;
    public bool lastGrounded = true;

    [Header("References")]
    [SerializeField] private EnemyGroundCheck groundCheck;
    [SerializeField] private Rigidbody2D slimeRb;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip _SlimeJump;
    [SerializeField] private AudioClip _SlimeImpact;
    [SerializeField] private EnemyHit enemyHit;

    void Awake()
    {
        groundCheck._IsGrounded = false;
    }
    void OnEnable()
    {
        StopCoroutine(SlimeUpdate());
        StartCoroutine(SlimeUpdate());
    }
    void Start()
    {
        lastGrounded = true;
        slimeRb = GetComponent<Rigidbody2D>();
        // Ensure Gravity Scale is at least 1-2 so it falls back down!
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    public override void OnGroundTouch()
    {
        audioSource.PlaySound(_SlimeImpact,0.8f,Random.Range(0.7f,1.3f),1,1,transform.position);
    }
    public override void OnGroundLeave()
    {
    }
    public override void OnHit()
    {
        StartCoroutine(DamageAnimation());
    }
    void Update()
    {
        if (groundCheck._IsGrounded && Mathf.Abs(slimeRb.linearVelocityX) > 5 && !enemyHit._DamageTaken)
        {
            slimeRb.linearVelocityX = slimeRb.linearVelocityX > 0 ? 5 : -5;
        }
    }
    private IEnumerator SlimeUpdate()
    {
        float elapsed = 0;
        float timer = Random.Range(1.5f,2.0f);
        while(elapsed < timer)
        {
            elapsed += Time.deltaTime;
            if (floating)
            {
                elapsed = timer;
            }
            yield return null;
        }
        yield return new WaitUntil(() => currentState == State.Chase && (groundCheck._IsGrounded||floating));
        if (floating)
        {
            StartCoroutine(FloatTowardsPlayer());
        }
        else
        {
            StartCoroutine(JumpTowardsPlayer());
        }
    }

    private IEnumerator JumpTowardsPlayer()
    {
        // Calculate direction to player (Left or Right)
        slimeLeftOrRight = (player.position.x > transform.position.x) ? true : false;
        // if slimeLeftOrRight is true, then it's facing right, otherwise it's facing left
        anim.SetInteger("attackPhase",1);
        float elapsed = 0;
        while(elapsed < 0.75f)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitUntil(() => groundCheck._IsGrounded);
        anim.SetInteger("attackPhase",2);

        // Play slime jump sound
        audioSource.PlaySound(_SlimeJump,0.8f,Random.Range(0.7f,1.3f),1,1,transform.position);

        // Apply a diagonal "Hop" force
        slimeRb.AddForce(new Vector2((slimeLeftOrRight?1:-1) * forwardForce * waterSpeedMulti, jumpForce * waterJumpMulti), ForceMode2D.Impulse);
        elapsed = 0;
        while(elapsed < 0.1)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitUntil(() => groundCheck._IsGrounded||floating);
        anim.SetInteger("attackPhase",0);
        StartCoroutine(SlimeUpdate());
    }
    private IEnumerator FloatTowardsPlayer()
    {
        anim.SetInteger("attackPhase",3);
        float elapsed = 0;
        while (elapsed < 0.2f)
        {
            slimeLeftOrRight = (player.position.x > transform.position.x) ? true : false;
            slimeRb.AddForce(new Vector2((slimeLeftOrRight?1:-1) * forwardForce * waterSpeedMulti,0), ForceMode2D.Force);
            if(Mathf.Abs(slimeRb.linearVelocityX) > 5f)
            {
                slimeRb.linearVelocityX = (slimeLeftOrRight?1:-1) * 5f;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitUntil(() => groundCheck._IsGrounded||floating);
        if (floating)
        {
            StartCoroutine(FloatTowardsPlayer());
            yield break;
        }
        anim.SetInteger("attackPhase",0);
        StartCoroutine(SlimeUpdate());
    }
    private IEnumerator DamageAnimation()
    {
        anim.SetInteger("attackPhase",2);
        anim.SetTrigger("restartAnimation");
        float elapsed = 0;
        while(elapsed < 0.08)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        yield return new WaitUntil(() => groundCheck._IsGrounded||floating);
        anim.SetInteger("attackPhase",0);
    }
}
