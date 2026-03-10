using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator _Transition;
    public void TransitionToScene(int sceneNumber, float transitionTime)
    {
        StartCoroutine(LoadScene(sceneNumber, transitionTime));
    }
    IEnumerator LoadScene(int levelIndex, float transitionTime)
    {
        _Transition.SetTrigger("Transition");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
