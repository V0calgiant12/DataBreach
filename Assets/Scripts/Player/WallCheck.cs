using UnityEngine;

public class WallCheck : PlayerStateManager
{
    
    public static WallCheck Instance;
    public bool _IsClinging;
    [SerializeField] private bool isRightSide;

    void Start()
    {
        Instance = this;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Stay " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground") && GroundCheck.Instance._IsGrounded)
        {
            _IsClinging = true;
            if (isRightSide)
            {
                currentState.currentWallSide = true;
            }
            else
            {
                currentState.currentWallSide = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        //Debug.Log("Exit " + other.gameObject.CompareTag("Ground"));
        if (other.gameObject.CompareTag("Ground") && GroundCheck.Instance._IsGrounded)
        {
            _IsClinging = false;
        }
    }
}