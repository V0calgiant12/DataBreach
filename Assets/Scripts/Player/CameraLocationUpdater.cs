using System.Numerics;
using UnityEngine;

public class CameraLocationUpdater : MonoBehaviour 
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = new UnityEngine.Vector3(player.transform.position.x + rb.linearVelocityX/4, player.transform.position.y + rb.linearVelocityY/4, -10);
    }
}