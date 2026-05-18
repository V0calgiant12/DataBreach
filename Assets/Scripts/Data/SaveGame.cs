using UnityEngine;

public class SaveGame : MonoBehaviour
{
    void Awake()
    {
        GameData.Instance.SaveData();
    }
}
