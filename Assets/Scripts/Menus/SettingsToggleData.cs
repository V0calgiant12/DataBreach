using UnityEngine;
using UnityEngine.UI;

public class SettingsToggleData : MonoBehaviour
{
    public Toggle toggle;
    public int _ToggleID;
    public void RefreshVisuals() // Refreshes the toggle state to be up to date with the saves
    {
        switch (_ToggleID)
        {
            case (0):
                toggle.isOn = SettingsData.Instance._UpToJump;
                break;
            case (1):
                toggle.isOn = SettingsData.Instance._RunInBackground;
                break;
            case (2):
                toggle.isOn = SettingsData.Instance._Bloom;
                break;
            case (3):
                toggle.isOn = SettingsData.Instance._ChromaticAberration;
                break;
            case (4):
                toggle.isOn = SettingsData.Instance._Vignette;
                break;
            case (5):
                toggle.isOn = SettingsData.Instance._Pixelation;
                break;
        }
    }
}
