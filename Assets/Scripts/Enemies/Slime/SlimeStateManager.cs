using System.Collections;
using UnityEngine;

public class SlimeStateManager : MonoBehaviour
{
    public enum State { Idle, Chase, Dead}
    public State currentState = State.Idle;

    [Header("Jump Settings")]
    public float jumpForce = 5f;      // Upward power
    public float forwardForce = 3f;   // Horizontal power toward player
    public int timeBetweenJumps = 90;

    [Header("Detection")]
    public float detectionRange = 5f;
    public LayerMask groundLayer;

    [Header("Slime References")]
    public GameObject slimeTrigger;
    public Rigidbody2D slimeRb;
    public Transform player;
    public int jumpTimer;
    public float slimeHealth = 1f;
    public bool isGrounded;
    public bool slimeLeftOrRight;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    public AudioSource audioSource3;
    [SerializeField] private AudioSource audioSource4;
    public AudioClip _SlimeJump;
    public AudioClip _SlimeImpact;
    public AudioClip _SlimeAttack;
    public AudioClip _SlimeDeath;

    void Awake()
    {
        slimeTrigger.SetActive(false);
    }
    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        slimeHealth = 1;
        StartCoroutine(DelayStart());
        // Ensure Gravity Scale is at least 1-2 so it falls back down!
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(SlimeUpdate),1.5f,1.5f);
    }
    public IEnumerator DelayStart()
    {
        int elapsed = 0;
        while (elapsed <= 60)
        {
            elapsed += 1;
        }
        yield return null;
        slimeTrigger.SetActive(true);
        
    }
    void Update()
    {
        jumpTimer += 1;
    }
    private void SlimeUpdate()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // Jump Logic
        if (jumpTimer >= timeBetweenJumps && isGrounded)
        {
            if (currentState == State.Chase)
            {
                JumpTowardsPlayer();
            }
            jumpTimer = 0;
        }
        if (slimeHealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("slime is dead");
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
        audioSource.Play();
        //Debug.Log("jump");
        // Apply a diagonal "Hop" force
        Vector2 hopVector = new Vector2(direction * forwardForce, jumpForce);
        slimeRb.AddForce(hopVector, ForceMode2D.Impulse);
    }

    // Basic ground check using collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Spikes")||collision.gameObject.CompareTag("MovingPlatform")||collision.gameObject.CompareTag("Stone"))
        {
            isGrounded = true;
            audioSource2.Play();
            //Debug.Log("land");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Spikes")||collision.gameObject.CompareTag("MovingPlatform")||collision.gameObject.CompareTag("Stone"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = State.Chase;
        }
    }

    private void OnTriggerExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentState = State.Idle;
        }
    }
}
