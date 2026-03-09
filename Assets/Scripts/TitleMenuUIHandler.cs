using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class TitleMenuUIHandler : MonoBehaviour
{
    public GameObject settingsMenu;

    public void NewSaveButton()
    {
        SceneManager.LoadScene(1);
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
