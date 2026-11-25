using UnityEngine;

public class CameraFrameLimiter : MonoBehaviour
{
    void Start()
    {
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = 60;
    }
    void Update()
    {
	    QualitySettings.vSyncCount = SettingsData.Instance._VSync ? 1 : 0;
	    Application.targetFrameRate = SettingsData.Instance._MaxFrameRate == 500 ? -1 : (int) SettingsData.Instance._MaxFrameRate;
    }
}