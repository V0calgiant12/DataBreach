using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsData : MonoBehaviour
{
    /// <summary>
    /// This script handels save data for things that are changed in the settings menu.
    /// To add a new saved variable, first add it to this class, SettingsData, then add it to the SaveData class.
    /// Next, add the variable to be saved and loaded in their respective functions.
    /// </summary>
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
    public float _MasterVolume;
    public float _MusicVolume;
    public float _EffectsVolume;
    public float _DialogueVolume;
    
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
    public void SaveSettings() // Saves data to a JSON file.
    {
        SaveSettings data = new SaveSettings();

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

        data._MasterVolume = _MasterVolume;
        data._MusicVolume = _MusicVolume;
        data._EffectsVolume = _EffectsVolume;
        data._DialogueVolume = _DialogueVolume;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
    }
    public void LoadSettings() // Loads data from the JSON file.
    {
        string path = Application.persistentDataPath + "/settings.json";
        if (File.Exists(path)) // Checks to see if the file even exists before attempting to read from it.
        {
            string json = File.ReadAllText(path);
            SaveSettings data = JsonUtility.FromJson<SaveSettings>(json);
            
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
            
            _MasterVolume = data._MasterVolume;
            _MusicVolume = data._MusicVolume;
            _EffectsVolume = data._EffectsVolume;
            _DialogueVolume = data._DialogueVolume;
        }
    }
}

[System.Serializable]
class SaveSettings // This class quite literally just stores variables so they can be saved.
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
    [Header("Audio")]
    public float _MasterVolume;
    public float _MusicVolume;
    public float _EffectsVolume;
    public float _DialogueVolume;
    
}
