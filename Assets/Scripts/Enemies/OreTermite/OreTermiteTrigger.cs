using UnityEngine;

public class OreTermiteTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OreTermiteManager oreTermiteManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!oreTermiteManager.extended)
            {
                oreTermiteManager.Stab();
            }
            oreTermiteManager.playerInTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            oreTermiteManager.playerInTrigger = false;
        }
    }
}