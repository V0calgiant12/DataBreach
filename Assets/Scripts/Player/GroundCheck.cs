using UnityEngine;

public class GroundCheck : MonoBehaviour
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
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform"))
        {
            _IsGrounded = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        //Debug.Log("Exit " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform"))
        {
            _IsGrounded = false;
            if(PlayerStateManager.Instance.playerData.jumpBufferCounter < 0)
            {
                PlayerStateManager.Instance.playerData.coyoteTimeCounter = 15;
            }
        }
    }
}