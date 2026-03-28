using System.Drawing;
using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
    [SerializeField] private UnityEngine.Color playerColor;
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