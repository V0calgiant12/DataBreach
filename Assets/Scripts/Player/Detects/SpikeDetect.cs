using UnityEngine;

public class SpikeDetect : MonoBehaviour
{
    [Header("Spike References:")]
    public Rigidbody2D PlayerRb;
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {   
            if (PlayerStateManager.Instance.playerData.playerHealth > 0)
            {
                PlayerStateManager.Instance.DamagePlayer(10,20,30,true,transform.position.x,true);
            }
        }
    }
}