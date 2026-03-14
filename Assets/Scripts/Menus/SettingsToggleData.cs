using UnityEngine;
using UnityEngine.UI;

public class SettingsToggleData : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    public int _ToggleID;
    public void RefreshVisuals()
    {
        switch (_ToggleID)
        {
            case (0):
                toggle.isOn = SettingsData.Instance._UpToJump;
                break;
        }
    }
}
