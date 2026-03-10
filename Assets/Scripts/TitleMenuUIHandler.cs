using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class TitleMenuUIHandler : MonoBehaviour
{
    public GameObject settingsMenu;
    public SceneTransition sceneTransition;
    void Start()
    {
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
    }

    public void NewSaveButton()
    {
        sceneTransition.TransitionToScene(1,1);
    }
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
