using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class VolumetricEnabler : MonoBehaviour
{
    private enum Setting
    {
        Volumetrics,
        RunInBackground,
        ToggleSprint,
        ToggleCrouch,
        DoubleTapFastFall,
        Bloom,
        ChromaticAberration,
        Vignette,
        Pixelation
    }
    [SerializeField] private Setting settingToGet;
    [SerializeField] private bool settingEnabled;
    [SerializeField] private bool flipOutput;
    [SerializeField] private GameObject[] objects;
    void Start()
    {
        InvokeRepeating("OffsetUpdate",0,1f);
    }
    private void OffsetUpdate()
    {
        settingEnabled = GetSetting(settingToGet);
        if (flipOutput)
        {
            settingEnabled = !settingEnabled;
        }
        for(int i = 0; i < objects.Length; i++)
        {
            bool state = objects[i].activeInHierarchy;
            objects[i].SetActive(true);
            Light2D light = objects[i].GetComponent<Light2D>();
            light.volumetricEnabled = settingEnabled;
            objects[i].SetActive(state);
        }
    }
    private bool GetSetting(Setting setting)
    {
        switch (setting)
        {
            case(Setting.RunInBackground):
                return SettingsData.Instance._RunInBackground;
            case(Setting.ToggleSprint):
                return SettingsData.Instance._ToggleSprint;
            case(Setting.ToggleCrouch):
                return SettingsData.Instance._ToggleCrouch;
            case(Setting.DoubleTapFastFall):
                return SettingsData.Instance._DoubleTapFastFall;
            case(Setting.Bloom):
                return SettingsData.Instance._Bloom;
            case(Setting.ChromaticAberration):
                return SettingsData.Instance._ChromaticAberration;
            case(Setting.Vignette):
                return SettingsData.Instance._Vignette;
            case(Setting.Pixelation):
                return SettingsData.Instance._Pixelation;
            case(Setting.Volumetrics):
                return SettingsData.Instance._Volumetrics;
        }
        return false;
    }
}