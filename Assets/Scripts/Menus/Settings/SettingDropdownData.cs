using UnityEngine;
using TMPro;
using System;

public class SettingDropdownData : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int _DropdownID;
    public void RefreshVisuals() // Refreshes the toggle state to be up to date witht he saves
    {
        switch (_DropdownID)
        {
            case (0):
                dropdown.value = SettingsData.Instance._Fullscreen;
                break;
            case (1):
                dropdown.value = Convert.ToInt16(SettingsData.Instance._ToggleSprint);
                break;
            case (2):
                dropdown.value = SettingsData.Instance._Resolution;
                break;
            case (3):
                dropdown.value = Convert.ToInt16(SettingsData.Instance._ToggleCrouch);
                break;
        }
    }
}
