using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    public Rigidbody2D StalagmiteRb;
    public GameObject Collider;
    public GameObject Hitbox;
    public GameObject Trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StalagmiteRb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StalagmiteRb.bodyType = RigidbodyType2D.Dynamic;
            Trigger.SetActive(false);
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            Collider.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.playerHealth - 1f;
            Debug.Log(PlayerStateManager.Instance.playerData.playerHealth);
            Hitbox.SetActive(false);
        }
    }
    
}