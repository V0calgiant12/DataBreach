using UnityEngine;
public class NextScene : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        IntroCutsceneManager cutsceneManager = GameObject.Find("Cutscene").GetComponent<IntroCutsceneManager>();
        cutsceneManager.ProgressCutscene();
    }
}