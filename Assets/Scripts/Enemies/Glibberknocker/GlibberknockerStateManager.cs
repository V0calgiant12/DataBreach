using UnityEngine;

public class GlibberknockerStateManager : MonoBehaviour
{

    public Rigidbody2D glibberknockerRb;
    public Transform player;
    public bool isGrounded;
    public float glibberknockerHealth = 5f;

    [Header("Detection")]
    public float detectionRange = 5f;
    public LayerMask groundLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        glibberknockerRb = GetComponent<Rigidbody2D>();

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.CompareTag("Stone"))
        {
            isGrounded = true;
            //Debug.Log("grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Spikes") || collision.gameObject.CompareTag("MovingPlatform") || collision.gameObject.CompareTag("Stone"))
        {
            isGrounded = false;
        }
    }

}
