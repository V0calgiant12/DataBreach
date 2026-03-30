using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BitAI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    public float moveSpeed;
    void Start()
    {
        StartCoroutine(MoveTowardsPlayer());
    }
    void Update()
    {
        transform.Rotate(0,0,rb.linearVelocityX+rb.linearVelocityY);
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
        while(Vector2.Distance(transform.position,player.transform.position) > 2.5)
        {
            rb.linearVelocityX = moveSpeed*moveSpeed*moveSpeed * (player.transform.position.x - transform.position.x);
            rb.linearVelocityY = moveSpeed*moveSpeed*moveSpeed * (player.transform.position.y - transform.position.y);
            yield return null;
        }
    }
}