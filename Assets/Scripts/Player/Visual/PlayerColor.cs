using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
    [Header("Player Color Settings:")]
    [SerializeField] private UnityEngine.Color playerColor;
    public static float rainbowHue;
    public static UnityEngine.Color rainbowColor;
    public static bool rainbow = false;
    private bool coroutineRunning;
    [Header("Player Color References:")]
    [SerializeField] private SpriteRenderer sr;
    private enum UpdateTime
    {
        Always,
        EverySecond

    }
    [SerializeField] private UpdateTime updateTime;
    void OnEnable()
    {
        CancelInvoke("SetColor");
        if(updateTime == UpdateTime.EverySecond)
        {
            InvokeRepeating("SetColor",0,1);
        }
        else
        {
            SetColor();
        }
    }

    void Update()
    {
        if(updateTime == UpdateTime.Always)
        {
            SetColor();
        }
    }
    void SetColor()
    {
        if (!rainbow)
        {
            playerColor = UnityEngine.Color.HSVToRGB(SettingsData.Instance._PlayerHue, SettingsData.Instance._PlayerSaturation, SettingsData.Instance._PlayerValue);
            sr.color = new UnityEngine.Color(playerColor.r,playerColor.g,playerColor.b, sr.color.a);
        }
        else
        {
            StartRainbow();
        }
    }
    void StartRainbow()
    {
        if (!coroutineRunning)
        {
            rainbow = true;
            StartCoroutine(RainbowUpdate());
        }
    }
    IEnumerator RainbowUpdate()
    {
        bool direction = true;
        coroutineRunning = true;
        rainbowHue = 1;
        while (rainbow)
        {
            rainbowHue += (direction ? 0.5f : -0.5f) * Time.deltaTime;
            if(rainbowHue > 1 || rainbowHue < 0)
            {
                rainbowHue = rainbowHue > 0.5f ? 1 : 0;
            }
            if(rainbowHue == 1)
            {
                direction = false;
            }
            if(rainbowHue == 0)
            {
                direction = true;
            }
            rainbowColor = UnityEngine.Color.HSVToRGB(rainbowHue,1,1);
            sr.color = new UnityEngine.Color(rainbowColor.r,rainbowColor.g,rainbowColor.b, sr.color.a);
            yield return null;
        }
        coroutineRunning = false;
    }
}