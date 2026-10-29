using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInChildren<SceneTransition>().StartCoroutine(animator.GetComponentInChildren<SceneTransition>().ReloadScene());
    }
}