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
    [SerializeField] private Transform Enemies;
    [SerializeField] private int Id;
    void Awake()
    {
        ObjectsAndEnemies.SetActive(false);
        Grid.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject + " entered loading area.", this);
        if (other.gameObject.CompareTag("RealCamera"))
        {
            Debug.Log("Loading New Area.", this);
            ObjectsAndEnemies.SetActive(true);
            Grid.SetActive(true);   
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.transform.SetParent(Enemies);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.gameObject + " exited loading area.", this);
        if (other.gameObject.CompareTag("RealCamera"))
        {
            ObjectsAndEnemies.SetActive(false);
            Grid.SetActive(false);   
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
               
        }
    }
}