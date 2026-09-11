using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnableIfSetting : MonoBehaviour
{
    private enum Setting
    {
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
        InvokeRepeating("OffsetUpdate",0,1);
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
            Debug.Log(settingEnabled);
            objects[i].SetActive(settingEnabled);
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
        }
        return false;
    }
}