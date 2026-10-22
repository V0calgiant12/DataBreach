using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentLightIdDisplay : MonoBehaviour
{
    public int avgFrameRate;
    public TextMeshProUGUI display_Text;

    public void OnGUI()
    {
        display_Text.text = "Current Light Id: " + LightingAreaTrigger.activeLightId;
    }
}