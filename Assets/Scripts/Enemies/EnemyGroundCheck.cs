using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    [Header("Ground Check References:")]

    public bool _IsGrounded;
    public bool _IsStone;

    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone") && !_IsGrounded)
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
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone"))
        {
            _IsGrounded = false;
        }
        if (other.gameObject.CompareTag("Stone") && PlayerStateManager.Instance.playerData.coyoteTimeCounter < 0)
        {
            _IsStone = false;
        }
    }
}