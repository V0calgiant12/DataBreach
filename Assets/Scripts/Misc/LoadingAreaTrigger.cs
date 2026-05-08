using UnityEngine;

public class LoadingAreaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject ObjectsAndEnemies;
    [SerializeField] private GameObject Grid;
    void Start()
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