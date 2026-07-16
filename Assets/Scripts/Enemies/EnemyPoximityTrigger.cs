using UnityEngine;

public class EnemyPoximityTrigger : MonoBehaviour
{
    [Header("Info")]
    public bool playerDetected = false;
    public bool trigger = false; // Can be used in other scripts as a trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("collision is player");
            playerDetected = true;
            trigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDetected = false;
            trigger = true;
        }
    }
}