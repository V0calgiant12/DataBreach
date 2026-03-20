using UnityEngine;
using UnityEngine.UI;

public class SettingSliderData : MonoBehaviour
{
    public Slider slider;
    public float _DefaultValue = 0.5f;
    public int _SliderID;
    public void RefreshVisuals() // Refreshes the slider state to be up to date with the saves
    {
        switch (_SliderID)
        {
            case (0):
                slider.value = SettingsData.Instance._CameraZoom;
                break;
        }
    }
}
