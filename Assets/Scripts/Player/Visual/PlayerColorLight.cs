using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerColorLight : MonoBehaviour 
{
    [Header("Player Color Settings:")]
    [SerializeField] private UnityEngine.Color playerColor;
    [Header("Player Color References:")]
    [SerializeField] private Light2D light2D;
    void OnEnable()
    {
        CancelInvoke("OffsetUpdate");
        InvokeRepeating("OffsetUpdate",0,1);
    }

    void OffsetUpdate()
    {
        playerColor = UnityEngine.Color.HSVToRGB(SettingsData.Instance._PlayerHue, SettingsData.Instance._PlayerSaturation, SettingsData.Instance._PlayerValue);
        light2D.color = new UnityEngine.Color(playerColor.r,playerColor.g,playerColor.b, light2D.color.a);
    }
}