using UnityEngine;

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
            Debug.Log("Test");
            PlayerRb.AddForce(new Vector2(Random.Range(1, 5), Random.Range(1, 5)), ForceMode2D.Impulse);
        }
    }
}
