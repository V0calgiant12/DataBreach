using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Forest()
    {
        audioSource.Play();
        sceneTransition.TransitionToScene(4,1);
    }
    public void Mountains()
    {
        audioSource.Play();
        sceneTransition.TransitionToScene(5,1);
    }
}