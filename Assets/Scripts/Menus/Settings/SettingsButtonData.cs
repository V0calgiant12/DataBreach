using TMPro;
using UnityEngine;

public class SettingsButtonData : MonoBehaviour
{
    public int _SettingID;
    public GameObject _TextID;
    public TextMeshProUGUI _TextMesh;
    public KeyCode _DefaultBind;
    void Awake()
    {
        _TextMesh.text = "" + _DefaultBind; // Failsafe to default bind if there is no button to display. Happens before everything else.
    }
    public void RefreshVisuals() // Refreshes the text of all the keybind settings.
    {
        switch(_SettingID){
            case  0:
                _TextMesh.text = "" + SettingsData.Instance._InputLeft;
                break;
            case  1:
                _TextMesh.text = "" + SettingsData.Instance._InputRight;
                break;
            case  2:
                _TextMesh.text = "" + SettingsData.Instance._InputUp;
                break;
            case  3:
                _TextMesh.text = "" + SettingsData.Instance._InputDown;
                break;
            case  4:
                _TextMesh.text = "" + SettingsData.Instance._InputJump;
                break;
            case  5:
                _TextMesh.text = "" + SettingsData.Instance._InputSprint;
                break;
            case  6:
                _TextMesh.text = "" + SettingsData.Instance._InputAttack;
                break;
            case  7:
                _TextMesh.text = "" + SettingsData.Instance._InputParry;
                break;
            case  8:
                _TextMesh.text = "" + SettingsData.Instance._InputInteract;
                break;
        }
    }
}
