using System.Collections;
using UnityEngine;

public class CameraUpdater : MonoBehaviour 
{
    [SerializeField] private GameObject cameraLoc;
    private Vector3 Velocity = Vector3.zero;
    void Start()
    {
	    QualitySettings.vSyncCount = 0;
	    Application.targetFrameRate = 60;
        StartCoroutine(RunFrameTwo());
    }
    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraLoc.transform.position, ref Velocity, 0.2f);
    }
    IEnumerator RunFrameTwo()
    {
        int elapsed = 0;
        elapsed += 1;
        if(elapsed != 2)
        {
            yield return null;
        }
        transform.position = cameraLoc.transform.position;
    }
}