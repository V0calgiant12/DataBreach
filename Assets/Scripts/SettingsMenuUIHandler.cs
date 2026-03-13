using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

public class SettingsMenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject titleMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject audioMenu;
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
        titleMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
