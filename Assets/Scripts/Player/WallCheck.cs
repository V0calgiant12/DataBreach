using UnityEngine;

public class WallCheck : MonoBehaviour
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
                PlayerStateManager.Instance.currentState.currentWallSide = true;
            }
            else
            {
                PlayerStateManager.Instance.currentState.currentWallSide = false;
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