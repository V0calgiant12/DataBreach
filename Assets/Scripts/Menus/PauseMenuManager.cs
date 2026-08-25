using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
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
        if (UserInput.Instance.MenuInput && !playerData.playerDead)
        {
            if (!canvas.activeSelf)
            {
                Pause();
            }
            else
            {
                ReturnButton();
            }
        }
    }
    public void Pause()
    {
        canvas.SetActive(true);
        Time.timeScale = 0;
        sceneTransition.transition.updateMode = AnimatorUpdateMode.UnscaledTime;
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
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
}
