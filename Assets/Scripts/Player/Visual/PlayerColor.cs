using System.Drawing;
using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
    [Header("Player Color Settings:")]
    [SerializeField] private UnityEngine.Color playerColor;
    [Header("Player Color References:")]
    [SerializeField] private SpriteRenderer sr;
    void Start()
    {
    }

    void Update()
    {
        playerColor = UnityEngine.Color.HSVToRGB(SettingsData.Instance._PlayerHue, SettingsData.Instance._PlayerSaturation, SettingsData.Instance._PlayerValue);
        sr.color = new UnityEngine.Color(playerColor.r,playerColor.g,playerColor.b, sr.color.a);
    }
}