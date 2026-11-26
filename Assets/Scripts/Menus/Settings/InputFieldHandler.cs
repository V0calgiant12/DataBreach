using TMPro;
using UnityEngine;

public class InputFieldHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField prompt;
    public void OpenPrompt()
    {
        UserInput.Instance._playerInput.actions.Disable();
        prompt.Select();
        prompt.ActivateInputField();
    }
    public void ClosePrompt()
    {
        prompt.Select();
        UserInput.Instance._playerInput.actions.Enable();
    }
}