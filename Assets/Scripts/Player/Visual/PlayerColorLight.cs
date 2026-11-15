using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerColorLight : MonoBehaviour 
{
    [Header("Player Color Settings:")]
    [SerializeField] private UnityEngine.Color playerColor;
    [Header("Player Color References:")]
    [SerializeField] private Light2D light2D;
    private enum UpdateTime
    {
        Always,
        EverySecond

    }
    void Update()
    {
        if(updateTime == UpdateTime.Always)
        {
            SetColor();
        }
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
    void SetColor()
    {
        if (!PlayerColor.rainbow)
        {
            playerColor = UnityEngine.Color.HSVToRGB(SettingsData.Instance._PlayerHue, SettingsData.Instance._PlayerSaturation, SettingsData.Instance._PlayerValue);
        }
        else
        {
            playerColor = PlayerColor.rainbowColor;
        }
        light2D.color = new UnityEngine.Color(playerColor.r,playerColor.g,playerColor.b, light2D.color.a);
    }
}