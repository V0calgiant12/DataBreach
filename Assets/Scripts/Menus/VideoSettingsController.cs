using System;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettingsController : MonoBehaviour
{
    [SerializeField] private GameObject videoMenu;
    public int _Fullscreen;
    public int _Resolution;
    public bool _Bloom;
    public bool _ChromaticAberration;
    public bool _Vignette;
    public bool _Pixelation;
    
    void Start() // Refreshes settings on load.
    {
        videoMenu.SetActive(true);
        RefreshSettings();
        videoMenu.SetActive(false);
    }
    public void SetFullscreenMode(int mode)
    {
        switch(mode){
            case(0):
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case(1):
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; 
                break;
            case(2):
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case(3):
                Screen.fullScreenMode = FullScreenMode.Windowed; 
                break;
        }
    }
    public void SetResolution(int res)
    {
        switch (res)
        {
            case(0):
                Screen.SetResolution(3840,2160,Screen.fullScreenMode);
                break;
            case(1):
                Screen.SetResolution(2560,1440,Screen.fullScreenMode);
                break;
            case(2):
                Screen.SetResolution(1920,1080,Screen.fullScreenMode);
                break;
            case(3):
                Screen.SetResolution(1366,768,Screen.fullScreenMode);
                break;
            case(4):
                Screen.SetResolution(1280,720,Screen.fullScreenMode);
                break;
            case(5):
                Screen.SetResolution(1024,768,Screen.fullScreenMode);
                break;
            case(6):
                Screen.SetResolution(640,480,Screen.fullScreenMode);
                break;
            case(7):
                Screen.SetResolution(720,578,Screen.fullScreenMode);
                break;
            case(8):
                Screen.SetResolution(720,480,Screen.fullScreenMode);
                break;
        }
    }
    
    public void DropdownSetting(SettingDropdownData data)
    {
        switch (data._DropdownID)
        {
            case(0):
                _Fullscreen = data.dropdown.value;
                SetFullscreenMode(data.dropdown.value);
                break;
            case(2):
                _Resolution = data.dropdown.value;
                SetResolution(data.dropdown.value);
                break;
        }
    }
    public void ToggleSetting(SettingsToggleData data)
    {
        switch(data._ToggleID)
        {
            //case 0 taken by Up To Jump in Controls menu
            //case 1 taken by Run In Background in Game menu
            case(2): // Run in background
                _Bloom = data.toggle.isOn;
                break;
            case(3): // Run in background
                _ChromaticAberration = data.toggle.isOn;
                break;
            case(4): // Run in background
                _Vignette = data.toggle.isOn;
                break;
            case(5): // Run in background
                _Pixelation = data.toggle.isOn;
                break;
        }
    }
    private void RefreshSettings() // Gets the saved settings and tells all other setting objects with the tag "GameMenu" to refresh their visuals, which is handled elsewhere.
    {
        GameObject[] videoMenuItems = GameObject.FindGameObjectsWithTag("VideoMenu"); // Puts all game menu objects in a list.
        int index = 0;
        while (index <= videoMenuItems.Length - 1) // Repeats for every game object.
        {
            videoMenuItems[index].SendMessage("RefreshVisuals");
            index += 1;
        }
    }
}
