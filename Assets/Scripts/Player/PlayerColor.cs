using System.Drawing;
using UnityEngine;

public class PlayerColor : MonoBehaviour 
{
    [SerializeField] private UnityEngine.Color playerColor;
    [SerializeField] private SpriteRenderer sr;
    void Start()
    {
        UpdateColor();
    }

    public void UpdateColor()
    {
        playerColor = UnityEngine.Color.HSVToRGB(SettingsData.Instance._PlayerHue, SettingsData.Instance._PlayerSaturation, SettingsData.Instance._PlayerValue);
        sr.color = playerColor;
    }
}