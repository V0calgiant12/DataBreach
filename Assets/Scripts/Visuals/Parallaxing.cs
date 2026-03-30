using UnityEngine;

[ExecuteAlways]
public class Parallaxing : MonoBehaviour
{
    [SerializeField] private GameObject cameraPos;
    public Vector2 _Offset = new Vector2(0,0);
    public float parallaxX = 5;
    public float parallaxY = 5;
    void Update()
    {
        transform.position = new Vector2((cameraPos.transform.position.x + _Offset.x) / parallaxX, (cameraPos.transform.position.y + _Offset.y) / parallaxY);
    }
}