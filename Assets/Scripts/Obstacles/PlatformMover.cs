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
    [SerializeField] private Vector3 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Slime = GameObject.FindGameObjectWithTag("Slime");
        nextPos = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, nextPos));
        transform.position = Vector2.MoveTowards(transform.position, nextPos, moveSpeed);
        if (Vector2.Distance(transform.position, nextPos) < 0.01f)
        {
            nextPos = (nextPos == pointA.position) ? pointB.position : pointA.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
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
        Player.transform.parent = null;
        Slime.transform.parent = null;
    }
}