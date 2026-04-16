using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorScreen : MonoBehaviour 
{
    [Header("Player Color Screen References:")]
    [SerializeField] private UnityEngine.Color playerColor;
    [SerializeField] private Image image;
    [SerializeField] private Image parentImage;
    void Start()
    {
    }

    void Update()
    {
        playerColor = UnityEngine.Color.HSVToRGB(SettingsData.Instance._PlayerHue, SettingsData.Instance._PlayerSaturation, SettingsData.Instance._PlayerValue);
        image.color = new UnityEngine.Color(playerColor.r,playerColor.g,playerColor.b, parentImage.color.a);
    }
}