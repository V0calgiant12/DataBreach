using Unity.VisualScripting;
using UnityEngine;

public class ParaSlime : MonoBehaviour
{
    public float platformSpeed = 1;
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 0.1f;
    [SerializeField] private Vector3 nextPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPos = pointB.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(Vector2.Distance(transform.position, nextPos));
        transform.position = Vector2.MoveTowards(transform.position, nextPos, moveSpeed);
        if (Vector2.Distance(transform.position, nextPos) < 0.01f)
        {
            nextPos = (nextPos == pointA.position) ? pointB.position : pointA.position;
        }
    }
}