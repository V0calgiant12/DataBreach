using UnityEngine;

public class LoadingAreaTrigger : MonoBehaviour
{
    /// <summary>
    /// This to check when not working:
    /// - Check that the variables are assigned to the right objects.
    /// - Check that the trigger areas are set to actually be triggers and not colliders.
    /// - Check that the trigger areas are set to the Trigger layer.
    /// - Check that the triggers ignore everything that isn't the Camera layer, Both Box Colliders and Rigidbodies.
    /// </summary>
    /// <returns></returns>
    [SerializeField] private GameObject ObjectsAndEnemies;
    [SerializeField] private GameObject Grid;
    void Awake()
    {
        ObjectsAndEnemies.SetActive(false);
        Grid.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject + " entered loading.", this);
        if (other.gameObject.CompareTag("RealCamera"))
        {
            Debug.Log("Loading Area.", this);
            ObjectsAndEnemies.SetActive(true);
            Grid.SetActive(true);   
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RealCamera"))
        {
            ObjectsAndEnemies.SetActive(false);
            Grid.SetActive(false);   
        }
    }
}