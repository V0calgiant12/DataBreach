using System.IO;
using UnityEngine;

public class SettingsData : MonoBehaviour
{
    /// <summary>
    /// This script handels save data for things that are changed in the settings menu.
    /// To add a new saved variable, first add it to this class, SettingsData, then add it to the SaveData class.
    /// Next, add the variable to be saved and loaded in their respective functions.
    /// </summary>
    public static SettingsData Instance;
    [Header("Controls")]
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
    public float _MasterVolume = 0.5f;
    public float _MusicVolume = 0.5f;
    public float _EffectsVolume = 0.5f;
    public float _DialogueVolume = 0.5f;
    [Header("Game")]
    public bool _RunInBackground = true;
    public bool _ToggleSprint = false;
    public float _CameraZoom = 10;
    public float _PlayerHue = 0;
    public float _PlayerSaturation = 1;
    public float _PlayerValue = 1;
    public int _Fullscreen = 0;
    
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

        data._RunInBackground = _RunInBackground;
        data._ToggleSprint = _ToggleSprint;
        data._CameraZoom = _CameraZoom;
        data._PlayerHue = _PlayerHue;
        data._PlayerSaturation = _PlayerSaturation;
        data._PlayerValue = _PlayerValue;
        data._Fullscreen = _Fullscreen;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/settings.json", json);
        Debug.Log("Settings saved to file!");
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
            
            _RunInBackground = data._RunInBackground;
            _ToggleSprint = data._ToggleSprint;
            _CameraZoom = data._CameraZoom;
            _PlayerHue = data._PlayerHue;
            _PlayerSaturation = data._PlayerSaturation;
            _PlayerValue = data._PlayerValue;
            _Fullscreen = data._Fullscreen;
            Application.runInBackground = _RunInBackground;
            Debug.Log("Loaded settings from file!");
        }
        else
        {
            NoFile();
        }
    }
    private void NoFile()
    {
        Debug.Log("No settings file found! Falling back to default settings.");
        
        _InputLeft = KeyCode.LeftArrow;
        _InputRight = KeyCode.RightArrow;
        _InputUp = KeyCode.UpArrow;
        _InputDown = KeyCode.DownArrow;
        _InputJump = KeyCode.Space;
        _InputSprint = KeyCode.X;
        _InputAttack = KeyCode.Z;
        _InputParry = KeyCode.V;
        _InputInteract = KeyCode.C;
        _UpToJump = false;
        
        _MasterVolume = 0.5f;
        _MusicVolume = 0.5f;
        _EffectsVolume = 0.5f;
        _DialogueVolume = 0.5f;

        _RunInBackground = true;
        _CameraZoom = 10;
        _PlayerHue = 0;
        _PlayerSaturation = 1;
        _PlayerValue = 1;
        _Fullscreen = 0;
        
        Application.runInBackground = _RunInBackground;
    }
}

[System.Serializable]
class SaveSettings // This class quite literally just stores variables so they can be saved.
{
    [Header("Controls")]
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
    public float _MasterVolume = 0.5f;
    public float _MusicVolume = 0.5f;
    public float _EffectsVolume = 0.5f;
    public float _DialogueVolume = 0.5f;
    [Header("Game")]
    public bool _RunInBackground = true;
    public bool _ToggleSprint = false;
    public float _CameraZoom = 10;
    public float _PlayerHue = 0;
    public float _PlayerSaturation = 1;
    public float _PlayerValue = 1;
    public int _Fullscreen = 0;
}
