using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    [Header("Ground Check References:")]

    public bool _IsGrounded;
    public bool _IsStone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone") && !_IsGrounded)
        {
            _IsGrounded = true;
            GetComponentInParent<EnemyAbstract>().OnGroundTouch();
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
            GetComponentInParent<EnemyAbstract>().OnGroundLeave();
        }
        if (other.gameObject.CompareTag("Stone"))
        {
            _IsStone = false;
        }
    }
}