using TMPro;
using UnityEngine;

public class InputFieldHandler : MonoBehaviour
{
    private bool promptOpen;
    [SerializeField] private TMP_InputField prompt;
    public void OpenPrompt()
    {
        UserInput.Instance._playerInput.actions.Disable();
        promptOpen = true;
        prompt.Select();
        prompt.ActivateInputField();
    }
    public void ClosePrompt()
    {
        promptOpen = false;
        prompt.Select();
        UserInput.Instance._playerInput.actions.Enable();
    }
}