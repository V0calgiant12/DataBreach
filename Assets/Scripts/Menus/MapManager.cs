using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;
    void Start()
    {
        
    }

    public void Forest()
    {
        sceneTransition.TransitionToScene(4,1);
    }
    public void Mountains()
    {
        sceneTransition.TransitionToScene(5,1);
    }
}