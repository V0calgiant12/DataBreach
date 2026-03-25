using System;
using UnityEngine;
using UnityEngine.UI;
public class GameSettingsController : MonoBehaviour
{
    [SerializeField] private GameObject gameMenu;
    [Header("Toggles")]
    public bool _RunInBackground = true;
    public bool _ToggleSprint = false;
    [Header("Values")]
    public float _CameraZoom;
    public float _PlayerHue;
    public float _PlayerSaturation;
    public float _PlayerValue;

    void Start() // Refreshes settings on load.
    {
        gameMenu.SetActive(true);
        RefreshSettings();
        gameMenu.SetActive(false);
    }
    public void ToggleSetting(SettingsToggleData data)
    {
        switch(data._ToggleID)
        {
            //case 0 taken by Up To Jump in controls menu
            case(1): // Run in background
                _RunInBackground = data.toggle.isOn;
                Application.runInBackground = _RunInBackground;
                break;
        }
    }
    public void DropdownSetting(SettingDropdownData data)
    {
        switch (data._DropdownID)
        {
            case(1):
                _ToggleSprint = data.dropdown.value == 1;
                break;
        }
    }
    public void SliderSetting(SettingSliderData data)
    {
        switch (data._SliderID)
        {
            case(0):
                _CameraZoom = data.slider.value;
                break;
            case(1):
                _PlayerHue = data.slider.value;
                break;
            case(2):
                _PlayerSaturation = data.slider.value;
                break;
            case(3):
                _PlayerValue = data.slider.value;
                break;
        }
    }
    public void SliderResetToDefault(SettingSliderData data)
    {
        data.slider.value = data._DefaultValue;
        SliderSetting(data);
    }
    private void RefreshSettings() // Gets the saved settings and tells all other setting objects with the tag "GameMenu" to refresh their visuals, which is handled elsewhere.
    {
        _RunInBackground = SettingsData.Instance._RunInBackground;

        GameObject[] gameMenuItems = GameObject.FindGameObjectsWithTag("GameMenu"); // Puts all game menu objects in a list.
        int index = 0;
        while (index <= gameMenuItems.Length - 1) // Repeats for every game object.
        {
            gameMenuItems[index].SendMessage("RefreshVisuals");
            index += 1;
        }
    }
}
