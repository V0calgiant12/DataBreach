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
        
        settingsMenu.SetActive(true); // Set entire settings menu to be awake
        settingsHandler.LoadSettings(); // Loads settings                           We have to do this because loading wont work if the objects aren't awake.
        settingsMenu.SetActive(false);// Set entire settings menu to not be awake
    }

    public void NewSaveButton() // Starts a transition to the Dev scene
    {
        sceneTransition.TransitionToScene(1,1); // Dev scene, 1 second transition.
    }
    public void SettingsButton() // Switches to settings menu.
    {
        settingsMenu.SetActive(true);
        settingsHandler.OnAwake();
        gameObject.SetActive(false);
    }
    public void Exit() // Close the game.
    {
        sceneTransition.ExitButton();
    }
}
