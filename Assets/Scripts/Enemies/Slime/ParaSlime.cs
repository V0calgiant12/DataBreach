using Unity.VisualScripting;
using UnityEngine;

public class ParaSlime : MonoBehaviour
{
    [Header("Para Slime Settings:")]
    public float platformSpeed = 1;
    public float moveSpeed = 0.1f;
    [Header("Para Slime References:")]
    public Transform pointA;
    public Transform pointB;
    
    [SerializeField] private Vector3 nextPos;
    [SerializeField] private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPos = pointB.position;
        InvokeRepeating(nameof(PlaySound),0.5f,0.5f);
    }
    private void PlaySound()
    {
        audioSource.pitch = Random.Range(0.7f,1.3f);
        audioSource.Play();
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