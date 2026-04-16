using UnityEngine;
using System.Collections;

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
            PlayerStateManager.Instance.DamagePlayer(Random.Range(8, 13),Random.Range(13, 18),120,true);
        }
    }
}