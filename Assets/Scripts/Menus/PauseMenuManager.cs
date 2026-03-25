using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private PlayerColor playerColor;
    private SceneTransition sceneTransition;
    void Start()
    {
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
            else
            {
                ReturnButton();
            }
        }
    }
    public void MainMenuButton()
    {
        sceneTransition.TransitionToScene(0,1);
    }
    public void SettingsButton()
    {
        settingsMenu.SetActive(true);
        canvas.SetActive(false);
    }
    public void ReturnButton()
    {
        playerColor.UpdateColor();
        canvas.SetActive(false);
    }
}
