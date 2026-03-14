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
    [SerializeField] private KeybindsController keybinds; // The Keybinds Controller, has all the keybinds in it.
    void Start()
    {
        SettingsData.Instance.LoadSettings();
        LoadSettings(); // Loads from scene persistence
        controlsMenu.SetActive(true);
        audioMenu.SetActive(true);
    }
    public void ControlsButton()
    {
        audioMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }
    public void AudioButton()
    {
        controlsMenu.SetActive(false);
        audioMenu.SetActive(true);
    }

    public void BackButton()
    {
        SaveSettings(); // Saves for scene persistence
        SettingsData.Instance.SaveSettings(); // Saves for instance persistence
        titleMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    private void SaveSettings()
    {
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
    }
    public void LoadSettings()
    {
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
        // Tells audio sliders to load their settings.
        audioMenu.SetActive(true);
        GameObject[] audioMenuItems = GameObject.FindGameObjectsWithTag("AudioMenu");
        int index = 0;
        while (index <= 3)
        {
            audioMenuItems[index].SendMessage("LoadSettings");
            index += 1;
        }
        audioMenu.SetActive(false);
    }
}
