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
        playerData.lastCheckpoint = new Vector2(0,0);
        playerData.playerHealth = 5;
        sceneTransition.TransitionToScene(13,1); // Intro scene, 1 second transition.
    }
    public void ContinueButton()
    {
        playerData.lastCheckpoint = new Vector2(0,0);
        if(Input.GetKey(KeyCode.F3))
        {
            GameData.Instance.LoadData();
            sceneTransition.TransitionToScene(14,1);
            return;
        }
        if(GameData.Instance.SaveExists())
        {
            GameData.Instance.LoadData();
            sceneTransition.TransitionToScene(GameData.Instance._SceneId,1);
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
