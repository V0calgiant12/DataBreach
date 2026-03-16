using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    public GameObject BasicPlayer;
    public Rigidbody2D BasicRb;
    private Vector2 test;
    private float playerSpeed = 5;
    private float playerJump = 10;
    private float playerFastFallSpeed;
    private float playerFastFallMulti = 1.2f;
    private float velocityTest;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BasicRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerFastFallSpeed = -10 * playerFastFallMulti * (Time.deltaTime + 1);
        test.x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("jump");
            BasicRb.linearVelocity = new Vector2(BasicRb.linearVelocity.x, playerJump);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("fast fall");
            BasicRb.linearVelocity = new Vector2(BasicRb.linearVelocity.x, playerFastFallSpeed);
        }
    }
    private void FixedUpdate()
    {
        velocityTest = test.x * playerSpeed;
        BasicRb.linearVelocity = new Vector2(velocityTest, BasicRb.linearVelocity.y);
    }
}
