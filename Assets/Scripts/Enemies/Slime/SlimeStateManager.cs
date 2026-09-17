using System.Collections;
using UnityEngine;

public class SlimeStateManager : MonoBehaviour
{
    public enum State { Idle, Chase, Dead}
    public State currentState = State.Idle;

    [Header("Jump Settings")]
    public float jumpForce = 5f;      // Upward power
    public float mudJumpMulti = 1f;
    public float forwardForce = 1f;   // Horizontal power toward player
    public float mudSpeedMulti = 1f;
    public int timeBetweenJumps = 90;

    [Header("Detection")]
    public LayerMask groundLayer;

    [Header("Variables")]
    public int jumpTimer;
    public bool slimeLeftOrRight;
    public bool slimeSizeable = true;
    public bool lastGrounded = true;

    [Header("References")]
    [SerializeField] private GameObject slimeTrigger;
    [SerializeField] private EnemyGroundCheck groundCheck;
    [SerializeField] private Rigidbody2D slimeRb;
    [SerializeField] private Transform player;
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip _SlimeJump;
    [SerializeField] private AudioClip _SlimeImpact;
    [SerializeField] private AudioClip _SlimeAttack;
    [SerializeField] private AudioClip _SlimeDeath;
    [SerializeField] private EnemyHit enemyHit;

    void Awake()
    {
        groundCheck._IsGrounded = false;
        slimeTrigger.SetActive(false);
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
        if (slimeSizeable)
        {
            float scaleOffset = Random.Range(0.8f, 1.3f);
            transform.localScale = new Vector3(scaleOffset,scaleOffset,scaleOffset);
        }
        InvokeRepeating(nameof(SlimeUpdate), 1.5f, 1.5f+Random.Range(0.0f, 0.5f));
    }
    void Update()
    {
        jumpTimer += 1;
        if (!groundCheck._IsGrounded)
        {
            lastGrounded = false;
        }
        if (groundCheck._IsGrounded && !lastGrounded)
        {
            lastGrounded = true;
            audioSource.PlaySlimeJumpSound(_SlimeImpact);
        }
        if (groundCheck._IsGrounded && Mathf.Abs(slimeRb.linearVelocityX) > 5 && !enemyHit._DamageTaken)
        {
            slimeRb.linearVelocityX = slimeRb.linearVelocityX > 0 ? 5 : -5;
        }
    }
    private void SlimeUpdate()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        // Jump Logic
        if (jumpTimer >= timeBetweenJumps && groundCheck._IsGrounded)
        {
            if (currentState == State.Chase)
            {
                JumpTowardsPlayer();
            }
            jumpTimer = 0;
        }
    }

    void JumpTowardsPlayer()
    {
        // Calculate direction to player (Left or Right)
        float direction = (player.position.x > transform.position.x) ? 1f : -1f;
        // if slimeLeftOrRight is true, then it's facing right, otherwise it's facing left
        if (direction == 1f)
        {
            slimeLeftOrRight = true;
        }
        if (direction == -1f)
        {
            slimeLeftOrRight = false;
        }
        // Play slime jump sound
        audioSource.PlaySlimeJumpSound(_SlimeJump);

        //Debug.Log("jump");
        // Apply a diagonal "Hop" force
        Vector2 hopVector = new Vector2(direction * forwardForce * mudSpeedMulti, jumpForce * mudJumpMulti);
        slimeRb.AddForce(hopVector, ForceMode2D.Impulse);
    }
}
