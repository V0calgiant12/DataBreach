using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BitAI : MonoBehaviour
{
    [Header("Bit Settings:")]
    public float moveSpeed;
    public bool waitForPlayer = false;
    [Header("Bit References:")]
    [SerializeField] private GameObject playerOverride;
    [SerializeField] private Rigidbody2D rb;
    
    void Start()
    {
        if(!waitForPlayer)
        {
            StartCoroutine(MoveTowardsPlayer());
        }
    }
    void Update()
    {
        transform.Rotate(0,0,(rb.linearVelocityX+rb.linearVelocityY)*Time.timeScale);
    }
    public IEnumerator GlideTo(Vector2 location)
    {
        rb.linearVelocityX = moveSpeed*moveSpeed * (location.x - transform.position.x);
        rb.linearVelocityY = moveSpeed*moveSpeed * (location.y - transform.position.y);
        while(Vector2.Distance(transform.position,location) > 2.5)
        {
            yield return null;
        }
        rb.linearVelocity = new Vector2(0,0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(waitForPlayer && other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveTowardsPlayer());
            waitForPlayer = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveTowardsPlayer());
        }
    }
    private IEnumerator MoveTowardsPlayer()
    {
        GameObject player = playerOverride != null ? PlayerStateManager.Instance.gameObject : playerOverride;
        while(Vector2.Distance(transform.position,player.transform.position) > 2.5)
        {
            rb.linearVelocityX = moveSpeed*moveSpeed*moveSpeed * (player.transform.position.x - transform.position.x);
            rb.linearVelocityY = moveSpeed*moveSpeed*moveSpeed * (player.transform.position.y - transform.position.y);
            yield return null;
        }
    }
}