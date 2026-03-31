using System.Collections;
using UnityEngine;

public class SlimeStateManager : MonoBehaviour
{
    public enum State { Idle, Chase }
    public State currentState = State.Idle;

    [Header("Jump Settings")]
    public float jumpForce = 5f;      // Upward power
    public float forwardForce = 3f;   // Horizontal power toward player
    public int timeBetweenJumps = 90;

    [Header("Detection")]
    public float detectionRange = 5f;
    public LayerMask groundLayer;

    public Rigidbody2D slimeRb;
    public Transform player;
    public int jumpTimer;
    public bool isGrounded;

    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        // Ensure Gravity Scale is at least 1-2 so it falls back down!
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(SlimeUpdate),1.5f,1.5f);
    }
    void Update()
    {
        jumpTimer += 1;
    }
    private void SlimeUpdate()
    {
        float dist = Vector2.Distance(transform.position, player.position);

        // Simple State Switch
        currentState = (dist <= detectionRange) ? State.Chase : State.Idle;

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
