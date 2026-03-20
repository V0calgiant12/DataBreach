using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Audio;

public class SettingsMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject audioMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private KeybindsController keybinds; // The Keybinds Controller, has all settings under the controls tab in it.
    [SerializeField] private GameSettingsController gameSettings; // The Game Settings Controller, has all settings under the game tab in it.
    void Start() // Loads settings on load, just incase the player doesn't go into the settings before playing.
    {
        SettingsData.Instance.LoadSettings();
        LoadSettings(); // Loads from scene persistence
    }
    public void OnAwake() // Defaults to controls tab
    {
        controlsMenu.SetActive(true);
        audioMenu.SetActive(false);
        gameMenu.SetActive(false);
    }
    public void ControlsButton() // Switches to controls tab
    {
        audioMenu.SetActive(false);
        gameMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void AudioButton() // Switches to audio tab
    {
        controlsMenu.SetActive(false);
        gameMenu.SetActive(false);
        audioMenu.SetActive(true);
    }
    public void GameButton() // Switches to game tab
    {
        controlsMenu.SetActive(false);
        audioMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void BackButton() // Saves settings and switches back to main menu
    {
        SaveSettings(); // Saves for scene persistence
        SettingsData.Instance.SaveSettings(); // Saves for instance persistence
        titleMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    private void SaveSettings() // Saves settings to be loaded upon reloading scene or game.
    {
        // Controls Settings
        SettingsData.Instance._InputLeft = keybinds._InputLeft;
        SettingsData.Instance._InputRight = keybinds._InputRight;
        SettingsData.Instance._InputUp = keybinds._InputUp;
        SettingsData.Instance._InputDown = keybinds._InputDown;
        SettingsData.Instance._InputJump = keybinds._InputJump;
        SettingsData.Instance._InputSprint = keybinds._InputSprint;
        SettingsData.Instance._InputAttack = keybinds._InputAttack;
        SettingsData.Instance._InputParry = keybinds._InputParry;
        SettingsData.Instance._InputInteract = keybinds._InputInteract;
        SettingsData.Instance._UpToJump = keybinds._UpToJump;

        // Audio Settings
        // Tells audio sliders to save their settings.
        audioMenu.SetActive(true);
        GameObject[] audioMenuItems = GameObject.FindGameObjectsWithTag("AudioMenu");
        int index = 0;
        while (index <= 3)
        {
            audioMenuItems[index].SendMessage("SaveSettings");
            index += 1;
        }
        audioMenu.SetActive(false);

        // Game Settings
        SettingsData.Instance._RunInBackground = gameSettings._RunInBackground;
    }
    public void LoadSettings() // Fetches settings to load them.
    {
        audioMenu.SetActive(true);
        gameMenu.SetActive(true);
        // Controls Settings
        keybinds._InputLeft = SettingsData.Instance._InputLeft;
        keybinds._InputRight = SettingsData.Instance._InputRight;
        keybinds._InputUp = SettingsData.Instance._InputUp;
        keybinds._InputDown = SettingsData.Instance._InputDown;
        keybinds._InputJump = SettingsData.Instance._InputJump;
        keybinds._InputSprint = SettingsData.Instance._InputSprint;
        keybinds._InputAttack = SettingsData.Instance._InputAttack;
        keybinds._InputParry = SettingsData.Instance._InputParry;
        keybinds._InputInteract = SettingsData.Instance._InputInteract;
        keybinds._UpToJump = SettingsData.Instance._UpToJump;

        // Audio Settings
        // Tells audio sliders to load their settings.
        GameObject[] audioMenuItems = GameObject.FindGameObjectsWithTag("AudioMenu");
        int index = 0;
        while (index <= 3)
        {
            audioMenuItems[index].SendMessage("LoadSettings");
            index += 1;
        }
        

        // Game Settings
        gameSettings._RunInBackground = SettingsData.Instance._RunInBackground;
        gameSettings._CameraZoom = SettingsData.Instance._CameraZoom;
        gameSettings._Fullscreen = SettingsData.Instance._Fullscreen;
        gameSettings.SetFullscreenMode(SettingsData.Instance._Fullscreen);
        
        gameMenu.SetActive(false);
        audioMenu.SetActive(false);
    }
}
