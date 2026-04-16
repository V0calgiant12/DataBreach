using UnityEngine;
using static System.TimeZoneInfo;

public class SceneTransitionArea : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition; 
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered Transition Area");
            sceneTransition.TransitionToScene(0, 1);
        }
    }

}
