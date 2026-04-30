using UnityEngine;

public class SceneTransitionArea : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition; 
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int sceneId;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered Transition Area");
            sceneTransition.TransitionToScene(sceneId, 1);
            playerData.movementAllowed = false;
        }
    }

}
