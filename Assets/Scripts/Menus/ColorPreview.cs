using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorPreview : MonoBehaviour
{
    [SerializeField] private GameSettingsController gameSettings;
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private Color previewColor;
    [SerializeField] private SettingType ColorType;

    enum SettingType
    {
        Total,
        Saturation,
        Value
    }
    void OnGUI()
    {
        switch (ColorType)
        {
            case(SettingType.Total):
                previewColor = Color.HSVToRGB(gameSettings._PlayerHue, gameSettings._PlayerSaturation, gameSettings._PlayerValue);
                break;
            case(SettingType.Saturation):
                previewColor = Color.HSVToRGB(gameSettings._PlayerHue, 1, gameSettings._PlayerValue);
                break;
            case(SettingType.Value):
                previewColor = Color.HSVToRGB(gameSettings._PlayerHue, gameSettings._PlayerSaturation, 1);
                break;
        }
        image.color = previewColor;
    }
}
