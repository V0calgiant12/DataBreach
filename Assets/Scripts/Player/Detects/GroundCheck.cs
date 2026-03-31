using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static GroundCheck Instance;
    public bool _IsGrounded;
    public bool _IsStone;

    void Start()
    {
        Instance = this;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone"))
        {
            _IsGrounded = true;
        }
        if (other.gameObject.CompareTag("Stone"))
        {
            _IsStone = true;
        }
        else
        {
            _IsStone = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        //Debug.Log("Exit " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone"))
        {
            _IsGrounded = false;
            if(PlayerStateManager.Instance.playerData.jumpBufferCounter < 0)
            {
                PlayerStateManager.Instance.playerData.coyoteTimeCounter = 15;
            }
        }
        if (other.gameObject.CompareTag("Stone") && PlayerStateManager.Instance.playerData.coyoteTimeCounter < 0)
        {
            _IsStone = false;
        }
    }
}