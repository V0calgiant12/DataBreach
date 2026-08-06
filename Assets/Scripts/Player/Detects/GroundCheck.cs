using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Ground Check References:")]
    
    public bool _IsGrounded;
    public bool _IsStone;
    public bool _InCutscene = false;
    public static GroundCheck Instance;

    void Start()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_InCutscene && other.gameObject.CompareTag("Ground"))
        {
            TriggerShake.Instance.BurstShake(15,1,true);
        }
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
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("MovingPlatform")||other.gameObject.CompareTag("Stone"))
        {
            _IsGrounded = false;
        }
        if (!_InCutscene)
        {
            if (other.gameObject.CompareTag("Stone") && PlayerStateManager.Instance.playerData.coyoteTimeCounter < 0)
            {
                _IsStone = false;
            }
        }
    }
}