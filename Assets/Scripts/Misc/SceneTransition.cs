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
    private MusicManager music;
    public void TransitionToScene(int sceneNumber, float transitionTime)
    {
        StartCoroutine(LoadScene(sceneNumber, transitionTime));
    }
    IEnumerator LoadScene(int levelIndex, float transitionTime)
    {
        // NOTE: Transition time does not extend or shorten the fade animation. Fade animation is 1 second long. We can change this if we want later on.
        _Transition.SetTrigger("Transition");
        if(GameObject.Find("MusicController") != null)
        {
            music = GameObject.Find("MusicController").GetComponent<MusicManager>();
            music.FadeOutCaller(60);
        }
        Time.timeScale = 1;
        yield return new WaitForSeconds(transitionTime);
        renderFeatureToggler.DisableRenderFeatures();
        SceneManager.LoadScene(levelIndex);
    }
    public void ExitButton()
    {
        StartCoroutine(ExitFade());
    }
    IEnumerator ExitFade()
    {
        _Transition.SetTrigger("Transition");
        if(GameObject.Find("MusicController") != null)
        {
            music = GameObject.Find("MusicController").GetComponent<MusicManager>();
            music.FadeOutCaller(60);
        }
        yield return new WaitForSeconds(1);
        #if UNITY_EDITOR
            renderFeatureToggler.DisableRenderFeatures();
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
