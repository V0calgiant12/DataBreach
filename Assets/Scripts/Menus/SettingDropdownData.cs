using UnityEngine;
using TMPro;

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
        }
    }
}
