using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Moving Platform Settings:")]
    public float platformSpeed = 1;
    public float moveSpeed = 0.1f;
    [Header("Moving Platform References:")]
    public GameObject Player;
    public GameObject Slime;
    public Rigidbody2D platformRb;
    public Transform pointA;
    public Transform pointB;
    public Transform platform;
    [SerializeField] private Vector2 currentSpeed;
    [SerializeField] private Vector2 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Slime = GameObject.FindGameObjectWithTag("Enemy");
        currentSpeed = new Vector2(1,1);
        nextPos = pointB.position;
        StartCoroutine(MoveTowardsPoint());
    }

    // Update is called once per frame
    void Update()
    {
        if(platformRb.linearVelocityX == 0)
        {
            StartCoroutine(MoveTowardsPoint());
        }
    }
    private IEnumerator MoveTowardsPoint()
    {
        currentSpeed = new Vector2((nextPos.x - transform.position.x)/moveSpeed,(nextPos.y - transform.position.y)/moveSpeed);
        while(Vector2.Distance(transform.position,nextPos) > 1)
        {
            //Debug.Log(UnityEngine.Vector2.Distance(transform.position,nextPos));
            platformRb.linearVelocityX = currentSpeed.x;
            platformRb.linearVelocityY = currentSpeed.y;
            yield return null;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("GroundCheck"))
        {
            PlayerStateManager.Instance.playerData.OffsetVelocity.x = platformRb.linearVelocity.x;
        }
        if(other.gameObject.CompareTag("EnemyTrigger"))
        {
            //add enemy stuff later
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

       
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("GroundCheck"))
        {
            Player.transform.parent = null;
            PlayerStateManager.Instance.playerData.OffsetVelocity = new Vector2(0,0);
        }
        if(other.gameObject.CompareTag("EnemyTrigger"))
        {
            //other.gameObject.transform.parent = null;
        }
    }
}