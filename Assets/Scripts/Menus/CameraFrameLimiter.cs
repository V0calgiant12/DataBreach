using UnityEngine;

public class CameraFrameLimiter : MonoBehaviour
{
    void Start()
    {
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = 60;
    }
}