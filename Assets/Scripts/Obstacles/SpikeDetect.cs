using UnityEngine;
using System.Collections;

public class SpikeDetect : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {   
            PlayerStateManager.Instance.DamagePlayer(Random.Range(15, 20),Random.Range(15, 20),120,true);
        }
    }
}