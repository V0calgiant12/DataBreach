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
        _TextMesh.text = "" + _DefaultBind;
    }
}
