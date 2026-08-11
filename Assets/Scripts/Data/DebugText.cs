using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        text.text = UserInput.Instance.joystickName;
    }
}