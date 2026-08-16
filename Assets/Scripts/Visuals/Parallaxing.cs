using UnityEngine;
using System.Collections;

[ExecuteAlways]
public class Parallaxing : MonoBehaviour
{
    [SerializeField] private GameObject cameraPos;
    public Vector2 _Offset = new Vector2(0,0);
    private Vector3 Velocity = Vector3.zero;
    public float parallaxX = 5;
    public float parallaxY = 5;
    void Start()
    {
        transform.position = new Vector3((cameraPos.transform.position.x + _Offset.x) / parallaxX, (cameraPos.transform.position.y + _Offset.y) / parallaxY,0);
    }
    void LateUpdate()
    {
        Vector3 newPos = new Vector3((cameraPos.transform.position.x + _Offset.x) / parallaxX, (cameraPos.transform.position.y + _Offset.y) / parallaxY,0);
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref Velocity, 0.05f);
    }
}