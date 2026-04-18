using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class CameraLocationUpdater : MonoBehaviour 
{
    [Header("Camera Location Updater References:")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    private Camera mainCamera;
    public float limitX = float.NaN;
    private float cameraWidth;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    void Update()
    {
        cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        Debug.Log(transform.position.x + cameraWidth);
        transform.position = new UnityEngine.Vector3(player.transform.position.x + rb.linearVelocityX/4, player.transform.position.y + rb.linearVelocityY/4, -10);
        if(limitX != float.NaN && MathF.Abs(limitX) < transform.position.x + cameraWidth)
        {
            transform.position = new UnityEngine.Vector3(limitX - cameraWidth, transform.position.y, -10);
        }
    }
}