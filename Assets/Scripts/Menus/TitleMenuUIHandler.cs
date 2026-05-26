using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private SettingsMenuUIHandler settingsHandler;
    [SerializeField] private GameObject ContinueFade;
    private SceneTransition sceneTransition;
    void Start()
    {
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
        SettingsData.Instance.LoadSettings(); // Loads from instance persistence
        settingsMenu.SetActive(true); // Set entire settings menu to be awake
        settingsHandler.LoadSettings(); // Loads settings                           We have to do this because loading wont work if the objects aren't awake.
        settingsMenu.SetActive(false); // Set entire settings menu to not be awake
        if(GameData.Instance.SaveExists())
        {
            GameData.Instance.LoadData();
            ContinueFade.SetActive(false);
        }
    }

    public void NewSaveButton() // Starts a transition to the Intro scene
    {
        playerData.playerHealth = 5;
        sceneTransition.TransitionToScene(1,1); // Intro scene, 1 second transition.
    }
    public void ContinueButton()
    {
        Debug.Log("Test continue button");
        if(GameData.Instance.SaveExists())
        {
            GameData.Instance.LoadData();
            sceneTransition.TransitionToScene(GameData.Instance._SceneId,13);
        }
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
