using System;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuParallax : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float swayX = 0;
    static public float randomSwayX;
    [SerializeField] private float swayY = 0;
    static public float randomSwayY;
    public float swaySpeed;
    [SerializeField] private float timer;
    public float parallaxValue;
    public float offsetX;
    public float offsetY;
    void Start()
    {
        randomSwayX = UnityEngine.Random.Range(-1.0f, 1.0f);
        randomSwayY = UnityEngine.Random.Range(-1.0f, 1.0f);
    }
    void Update()
    {
        if(Input.mousePosition.x != 0 && Input.mousePosition.y != 0)
        {
            rectTransform.position = new Vector2((Input.mousePosition.x * parallaxValue)+(Screen.width/2)+offsetX,(Input.mousePosition.y * parallaxValue)+(Screen.height/2)+offsetY);
        }
        timer += Time.deltaTime;
        swayX = Mathf.Sin(timer * swaySpeed + randomSwayX) * 5;
        swayY = MathF.Cos(timer * swaySpeed + randomSwayY) * 5;
        rectTransform.position = new Vector2(rectTransform.position.x + swayX, rectTransform.position.y + swayY);
    }
}
