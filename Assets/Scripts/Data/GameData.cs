using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    /// <summary>
    /// This script handles saving data for the game
    /// To add a new saved variable, first add it to this class, SettingsData, then add it to the SaveData class.
    /// Next, add the variable to be saved and loaded in their respective functions.
    /// </summary>
    public static GameData Instance;
    [SerializeField] private PlayerData playerData;
    [Header("Save Version")]
    public int currentVersion;
    public int _SaveFileVersion = 0;
    [Header("Save Data")]
    public int _SceneId;
    public int _PlayerHealth;
    public int _MaxHealth;
    public bool _HasHeartCoin;
// Need level ID 
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
    public bool SaveExists()
    {
        string path = Application.persistentDataPath + "/gameData.json";
        if (File.Exists(path) && _SaveFileVersion == currentVersion) // Checks to see if the file even exists.
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SaveData() // Saves data to a JSON file.
    {
        SaveData data = new SaveData();

        //data.variable = variable;
        data._SaveFileVersion = currentVersion;
        data._PlayerHealth = playerData.playerHealth;
        data._MaxHealth = playerData.maxHealth;
        data._HasHeartCoin = playerData.hasHeartCoin;
        data._SceneId = _SceneId;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", json);
        
    }

    public void LoadData() // Loads data from the JSON file.
    {
        string path = Application.persistentDataPath + "/gameData.json";
        if (File.Exists(path)) // Checks to see if the file even exists before attempting to read from it.
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            
            //variable = data.variable;
            _SaveFileVersion = data._SaveFileVersion;
            _SceneId = data._SceneId;
            _PlayerHealth = data._PlayerHealth;
            _MaxHealth = data._MaxHealth;
            _HasHeartCoin = data._HasHeartCoin;
            if(data._MaxHealth < 5)
            {
                _PlayerHealth = 5;
            }
            if(data._PlayerHealth == 0)
            {
                _PlayerHealth = _MaxHealth;
            }
            playerData.playerHealth = _PlayerHealth;
            playerData.maxHealth = _MaxHealth;
            playerData.hasHeartCoin = _HasHeartCoin;
            Debug.Log("Data Exists");
        }
    }
    
}

[System.Serializable]
class SaveData // This class quite literally just stores variables so they can be saved.
{
    public int _PlayerHealth;
    public int _MaxHealth;
    public int _SceneId;
    public bool _HasHeartCoin;
    public int _SaveFileVersion = 0;
}

public class WaitForFrames : CustomYieldInstruction
{
    private int _targetFrameCount;
    
    public WaitForFrames(int numberOfFrames)
    {
        _targetFrameCount = Time.frameCount + numberOfFrames;
    }

    public override bool keepWaiting
    {
        get
        {
            return Time.frameCount < _targetFrameCount;
        }
    }
}