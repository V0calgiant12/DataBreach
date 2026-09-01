using UnityEngine;
public class EndFixedAnim : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("currentlyFixed", false);
    }
}