using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ColorPreview : MonoBehaviour
{
    [SerializeField] private GameSettingsController gameSettings;
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private Color previewColor;
    void OnGUI()
    {
        previewColor = Color.HSVToRGB(gameSettings._PlayerHue,gameSettings._PlayerSaturation,gameSettings._PlayerValue);
        image.color = previewColor;
    }
}
