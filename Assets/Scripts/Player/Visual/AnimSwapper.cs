using UnityEngine;

public class AnimSwapper : MonoBehaviour
{
    [Header("Player Animator References:")]
    public RuntimeAnimatorController normalAnim;
    public RuntimeAnimatorController airAnim;
    public Animator anim;
    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim != null && normalAnim != null)
        {
            anim.runtimeAnimatorController = normalAnim;
        }
    }
    void Update()
    {
        if (!GroundCheck.Instance._IsGrounded)
        {
            anim.runtimeAnimatorController = airAnim;
        }
        if (GroundCheck.Instance._IsGrounded)
        {
            anim.runtimeAnimatorController = normalAnim;

        }
    }
}
