using UnityEngine;

public class SettingsData : MonoBehaviour
{
    public static SettingsData Instance;
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
}
