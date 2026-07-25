using System.Collections;
using UnityEngine;

public class CameraUpdater : MonoBehaviour 
{
    [SerializeField] private GameObject cameraLoc;
    private Vector3 Velocity = Vector3.zero;
    void Start()
    {
	    QualitySettings.vSyncCount = 1;
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
        while(elapsed <= 2)
        {
            elapsed += 1;
            GoToPlayer();
            yield return null;
        }
    }
    public void GoToPlayer()
    {
        transform.position = cameraLoc.transform.position;
    }
}