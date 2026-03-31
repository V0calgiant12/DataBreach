using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public GameObject Player;
    public GameObject Slime;
    public float platformSpeed = 1;
    public Rigidbody2D platformRb;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 0.1f;
    private UnityEngine.Vector2 currentSpeed;
    [SerializeField] private UnityEngine.Vector2 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Slime = GameObject.FindGameObjectWithTag("Slime");
        nextPos = pointB.position;
        currentSpeed = new UnityEngine.Vector2(1,1);
        StartCoroutine(MoveTowardsPoint());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, nextPos));
    }
    private IEnumerator MoveTowardsPoint()
    {
        currentSpeed = new UnityEngine.Vector2((nextPos.x - transform.position.x)/moveSpeed,(nextPos.y - transform.position.y)/moveSpeed);
        while(UnityEngine.Vector2.Distance(transform.position,nextPos) > 1)
        {
            Debug.Log(UnityEngine.Vector2.Distance(transform.position,nextPos));
            platformRb.linearVelocityX = currentSpeed.x;
            platformRb.linearVelocityY = currentSpeed.y;
            yield return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PointA"))
        {
            nextPos = pointB.position;
            StartCoroutine(MoveTowardsPoint());
        }
        if(other.gameObject.CompareTag("PointB"))
        {
            nextPos = pointA.position;
            StartCoroutine(MoveTowardsPoint());
        }

        if(other.gameObject.CompareTag("GroundCheck"))
        {
            Player.transform.parent = transform;
        }
        if(other.gameObject.CompareTag("SlimeTrigger"))
        {
            Slime.transform.parent = transform;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("GroundCheck"))
        {
            Player.transform.parent = null;
        }
        if(other.gameObject.CompareTag("SlimeTrigger"))
        {
            Slime.transform.parent = null;
        }
    }
}