using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ControllerDetect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject parent;
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool ActivateWithController = true;
    private enum Function
    {
        Text,
        ControllerDisplay,
        ControllerType
    }
    [SerializeField] private Function scriptFunction;
    void Update()
    {
        switch (scriptFunction)
        {
            case(Function.Text):
                if(UserInput.Instance.currentController == UserInput.ControllerSchemes.Controller)
                {
                    text.enabled = ActivateWithController;
                }
                else
                {
                    text.enabled = !ActivateWithController;
                }
                break;
            case(Function.ControllerDisplay):
                if(UserInput.Instance.currentController == UserInput.ControllerSchemes.Controller)
                {
                    parent.SetActive(ActivateWithController);
                }
                else
                {
                    parent.SetActive(!ActivateWithController);
                }
                break;
            case(Function.ControllerType):
                switch (UserInput.Instance.controllerType)
                {
                    case(UserInput.ControllerTypes.Switch):
                        image.sprite = sprites[0];
                        break;
                    case(UserInput.ControllerTypes.Xbox):
                        image.sprite = sprites[1];
                        break;
                    case(UserInput.ControllerTypes.PS):
                        image.sprite = sprites[2];
                        break;
                }
                break;
        }
    }
}