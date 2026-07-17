using Unity.VisualScripting;
using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D playerRb;
    void Update()
    {
        if (GroundCheck.Instance._IsGrounded)
        {
            if(Mathf.Abs(playerRb.linearVelocityX) >= 0.1)
            {
                anim.SetBool("moving",true);
                if(Mathf.Abs(playerRb.linearVelocityX) > 8)
                {
                    anim.SetBool("sprinting", true);
                    anim.SetBool("walking",false);
                }
                else
                {
                    anim.SetBool("walking",true);
                    anim.SetBool("sprinting", false);
                }
            }
            else
            {
                anim.SetBool("moving", false);
            }
        }
    }
}