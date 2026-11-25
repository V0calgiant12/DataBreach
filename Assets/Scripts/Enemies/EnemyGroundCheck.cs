using System.Collections;
using UnityEngine;

public class EnemyGroundCheck : MonoBehaviour
{
    [Header("Ground Check References:")]

    public bool _IsGrounded;
    public bool _IsStone;
    public bool _IsWood;
    private bool delay = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone") || other.gameObject.CompareTag("Wood"))
        {
            _IsGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone") || other.gameObject.CompareTag("Wood") && !_IsGrounded)
        {
            _IsGrounded = true;
            if (!delay)
            {
                GetComponentInParent<EnemyAbstract>().OnGroundTouch();
                StartCoroutine(SmallDelay());
            }
        }
        if (other.gameObject.CompareTag("Stone"))
        {
            _IsStone = true;
        }
        else
        {
            _IsStone = false;
        }
        if (other.gameObject.CompareTag("Wood"))
        {
            _IsWood = true;
        }
        else
        {
            _IsWood = false;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Exit " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("MovingPlatform") || other.gameObject.CompareTag("Stone") || other.gameObject.CompareTag("Wood"))
        {
            _IsGrounded = false;
            if (gameObject.activeInHierarchy)
            {
                GetComponentInParent<EnemyAbstract>().OnGroundLeave();
            }
        }
        if (other.gameObject.CompareTag("Stone"))
        {
            _IsStone = false;
        }
        if (other.gameObject.CompareTag("Wood"))
        {
            _IsWood = false;
        }
    }
    public IEnumerator SmallDelay()
    {
        delay = true;
        yield return new WaitForSeconds(0.05f);
        delay = false;
    }
}