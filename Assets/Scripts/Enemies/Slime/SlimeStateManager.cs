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
    public bool isGrounded;
    public bool slimeLeftOrRight;
    public bool slimeSizeable = true;
    public bool noSlimeSound;

    [Header("References")]
    public GameObject slimeTrigger;
    public Rigidbody2D slimeRb;
    public Transform player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    public AudioClip _SlimeJump;
    public AudioClip _SlimeImpact;
    public AudioClip _SlimeAttack;
    public AudioClip _SlimeDeath;

    void Awake()
    {
        noSlimeSound = true;
        StartCoroutine(DelayStart2());
        slimeTrigger.SetActive(false);
    }
    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        StartCoroutine(DelayStart());
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
    public IEnumerator DelayStart()
    {
        int elapsed = 0;
        while (elapsed <= 60)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
        }
        yield return null;
        slimeTrigger.SetActive(true);
    }
    public IEnumerator DelayStart2()
    {
        noSlimeSound = true;
        int elapsed = 0;
        while (elapsed <= 999)
        {
            elapsed += Time.timeScale == 1 ? 1 : 0;
        }
        yield return null;
        noSlimeSound = false;
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
        if(!noSlimeSound)
        {
            audioSource.Play();
        }
        //Debug.Log("jump");
        // Apply a diagonal "Hop" force
        Vector2 hopVector = new Vector2(direction * forwardForce * mudSpeedMulti, jumpForce * mudJumpMulti);
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
}
