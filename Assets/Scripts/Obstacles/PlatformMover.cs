using Unity.VisualScripting;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float platformSpeed = 1;
    public Rigidbody2D platformRb;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 5f;
    [SerializeField] private Vector3 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platformRb = GetComponent<Rigidbody2D>();
        nextPos = pointB.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector2.Distance(transform.position, nextPos));
        transform.position = Vector2.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, nextPos) < 0.01f)
        {
            nextPos = (nextPos == pointA.position) ? pointB.position : pointA.position;
        }
    }
}