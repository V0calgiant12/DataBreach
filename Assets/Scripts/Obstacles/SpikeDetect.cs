using UnityEngine;
using System.Collections;

public class SpikeDetect : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    public float xLaunch;
    public float yLaunch;
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            xLaunch = Random.Range(10, 15);
            yLaunch = Random.Range(12, 20);
            PlayerStateManager.Instance.playerData.iFrames = 60;
            PlayerStateManager.Instance.playerData.movementAllowed = false;
            PlayerStateManager.Instance.DamagePlayer(xLaunch,yLaunch,120);
        }
    }
}