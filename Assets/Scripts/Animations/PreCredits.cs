using UnityEngine;
using UnityEngine.SceneManagement;
public class PreCredits : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(12);
    }
}