using UnityEngine;

public class SpikeDetect : MonoBehaviour
{
    public Rigidbody2D PlayerRb;
    void Start()
    {
        PlayerRb = gameObject.GetComponent<Rigidbody2D>();
        playerData.movementAllowed = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            Debug.Log("Test");
            PlayerRb.linearVelocity = new Vector2(transform.position.x * 99, 1);
            
        }
    }
}
