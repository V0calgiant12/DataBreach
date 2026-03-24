using UnityEngine;

public class GroundCheck : PlayerStateManager
{
    public static GroundCheck Instance;
    public bool _IsGrounded;

    void Start()
    {
        Instance = this;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground"))
        {
            _IsGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        //Debug.Log("Exit " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground"))
        {
            _IsGrounded = false;
            if(currentState.jumpBufferCounter < 0)
            {
                currentState.coyoteTimeCounter = 15;
            }
        }
    }
}