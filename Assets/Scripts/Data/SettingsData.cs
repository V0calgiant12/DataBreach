using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsData : MonoBehaviour
{
    public static SettingsData Instance;
    [Header("Keybinds")]
    public KeyCode _InputLeft = KeyCode.LeftArrow; // 0
    public KeyCode _InputRight = KeyCode.RightArrow; // 1
    public KeyCode _InputUp = KeyCode.UpArrow; // 2
    public KeyCode _InputDown = KeyCode.DownArrow; // 3
    public KeyCode _InputJump = KeyCode.Space; // 4
    public KeyCode _InputSprint = KeyCode.X; // 5
    public KeyCode _InputAttack = KeyCode.Z; // 6
    public KeyCode _InputParry = KeyCode.V; // 7
    public KeyCode _InputInteract = KeyCode.C; // 8
    public bool _UpToJump = false;
    [Header("Audio")]
    [SerializeField] private AudioMixer Mixer;
    public float MasterVolume;
    public float MusicVolume;
    public float EffectsVolume;
    public float DialogueVolume;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SaveSettings()
    {
        SaveData data = new SaveData();
        data._InputLeft = _InputLeft;
        data._InputRight = _InputRight;
        data._InputUp = _InputUp;
        data._InputDown = _InputDown;
        data._InputJump = _InputJump;
        data._InputSprint = _InputSprint;
        data._InputAttack = _InputAttack;
        data._InputParry = _InputParry;
        data._InputInteract = _InputInteract;
        data._UpToJump = _UpToJump;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }
    public void LoadSettings()
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            _InputLeft = data._InputLeft;
            _InputRight = data._InputRight;
            _InputUp = data._InputUp;
            _InputDown = data._InputDown;
            _InputJump = data._InputJump;
            _InputSprint = data._InputSprint;
            _InputAttack = data._InputAttack;
            _InputParry = data._InputParry;
            _InputInteract = data._InputInteract;
            _UpToJump = data._UpToJump;
        }
    }
}

[System.Serializable]
class SaveData
{
    [Header("Keybinds")]
    public KeyCode _InputLeft = KeyCode.LeftArrow; // 0
    public KeyCode _InputRight = KeyCode.RightArrow; // 1
    public KeyCode _InputUp = KeyCode.UpArrow; // 2
    public KeyCode _InputDown = KeyCode.DownArrow; // 3
    public KeyCode _InputJump = KeyCode.Space; // 4
    public KeyCode _InputSprint = KeyCode.X; // 5
    public KeyCode _InputAttack = KeyCode.Z; // 6
    public KeyCode _InputParry = KeyCode.V; // 7
    public KeyCode _InputInteract = KeyCode.C; // 8
    public bool _UpToJump = false;
    
}
