using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TitleMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private SettingsMenuUIHandler settingsHandler;
    private SceneTransition sceneTransition;
    void Start()
    {
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
        SettingsData.Instance.LoadSettings(); // Loads from instance persistence
        settingsMenu.SetActive(true);
        settingsHandler.LoadSettings();
        settingsMenu.SetActive(false);
    }

    public void NewSaveButton()
    {
        sceneTransition.TransitionToScene(1,1);
    }
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        settingsHandler.OnAwake();
        gameObject.SetActive(false);
    }
    public void Exit()
    {
        sceneTransition.ExitButton();
    }
}
