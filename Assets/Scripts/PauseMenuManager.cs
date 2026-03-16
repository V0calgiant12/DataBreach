using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject _Canvas;
    private SceneTransition sceneTransition;
    void Start()
    {
        sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_Canvas.activeSelf)
            {
                _Canvas.SetActive(true);
            }
            else
            {
                _Canvas.SetActive(false);
            }
        }
    }
    public void MainMenuButton()
    {
        sceneTransition.TransitionToScene(0,1);
    }
}
