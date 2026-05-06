using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SceneTransition : MonoBehaviour
{
    [Header("Scene Transition References:")]
    public Animator _Transition;
    [SerializeField] private RenderFeatureToggler renderFeatureToggler;
    public void TransitionToScene(int sceneNumber, float transitionTime)
    {
        StartCoroutine(LoadScene(sceneNumber, transitionTime));
    }
    IEnumerator LoadScene(int levelIndex, float transitionTime)
    {
        // NOTE: Transition time does not extend or shorten the fade animation. Fade animation is 1 second long. We can change this if we want later on.
        _Transition.SetTrigger("Transition"); 
        Time.timeScale = 1;
        yield return new WaitForSeconds(transitionTime);
        renderFeatureToggler.DisableRenderFeatures();
        SceneManager.LoadScene(levelIndex);
        switch (levelIndex)
        {
            case(3):
                // current level to 1 (Plains)
                break;
            case(4):
                // current level to 2 (Forest)
                break;
            case(5):
                //  current level to 3 (Mountains)
                break;
            case(6):
                // current level to 4 (Quarry)
                break;
            case(7):
                // current level to 5 (Deep Forest)
                break;
            case(8):
                // current level to 6 (Corrupted Plains)
                break;
        }
    }
    public void ExitButton()
    {
        StartCoroutine(ExitFade());
    }
    IEnumerator ExitFade()
    {
        _Transition.SetTrigger("Transition");
        yield return new WaitForSeconds(1);
        #if UNITY_EDITOR
            renderFeatureToggler.DisableRenderFeatures();
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
