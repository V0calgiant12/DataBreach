using UnityEngine;

public class SawbladeHitbox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            PlayerStateManager.Instance.DamagePlayer(10, Random.Range(6,10),60,false);
        }
    }
}
