using System.IO;
using UnityEngine;

public class GameData : MonoBehaviour
{
    /// <summary>
    /// This script handels
    /// To add a new saved variable, first add it to this class, SettingsData, then add it to the SaveData class.
    /// Next, add the variable to be saved and loaded in their respective functions.
    /// </summary>
    public static GameData Instance;

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
    public void SaveData() // Saves data to a JSON file.
    {
        SaveData data = new SaveData();

        //data.variable = variable;

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
        }
    }
    
}

[System.Serializable]
class SaveData // This class quite literally just stores variables so they can be saved.
{
    
}