using System;
using Unity.Mathematics;
using UnityEngine;

public class CameraCollisionUpdater : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Camera mainCamera;
    void Start()
    {
        
    }
    void Update()
    {
        boxCollider.size = new Vector2(mainCamera.orthographicSize * mainCamera.aspect * 2, mainCamera.orthographicSize * 2);
    }
}