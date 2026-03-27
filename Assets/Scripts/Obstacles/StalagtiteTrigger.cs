using UnityEngine;

public class Stalagtite : MonoBehaviour
{
    public Rigidbody2D StalagmiteRb;
    public GameObject Collider;
    public GameObject Hitbox;
    public GameObject Trigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("Hitbox"))
        {
            PlayerStateManager.Instance.playerData.playerHealth = PlayerStateManager.Instance.playerData.playerHealth - 1f;
            Debug.Log("Stalagtite Damaged Player");
        }
        if(other.gameObject.CompareTag("Ground") && gameObject.CompareTag("Hitbox"))
        {
            Hitbox.SetActive(false);
            Collider.SetActive(true);
            StalagmiteRb.bodyType = RigidbodyType2D.Static;
        }
        if(other.gameObject.CompareTag("Player") && !gameObject.CompareTag("Hitbox"))
        {
            StalagmiteRb.bodyType = RigidbodyType2D.Dynamic;
            Trigger.SetActive(false);
            Collider.SetActive(false);
            Hitbox.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
    }
    
}