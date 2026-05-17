using UnityEngine;
public class NextScene : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CutsceneManager cutsceneManager = GameObject.Find("Cutscene").GetComponent<CutsceneManager>();
        cutsceneManager.ProgressCutscene();
    }
}