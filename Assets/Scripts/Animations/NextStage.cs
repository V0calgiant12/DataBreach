using UnityEditor.SceneManagement;
using UnityEngine;
public class NextStage : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Stage",animator.GetInteger("Stage") + 1);
        if (animator.GetInteger("Stage") == 4)
        {
            animator.SetInteger("Stage",0);
        }
        //Debug.Log(animator.GetInteger("Stage"));
    }
}