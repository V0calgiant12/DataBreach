using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathScreen : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}