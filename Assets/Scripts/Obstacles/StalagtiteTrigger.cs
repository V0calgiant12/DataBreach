using UnityEngine;

public class StalagtiteTrigger : MonoBehaviour
{
    public Rigidbody2D StalagmiteRb;
    public GameObject Collider;
    public GameObject Hitbox;
    public GameObject Trigger;
    [SerializeField] private AudioSource audioSource;
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
        if((other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Stone")) && gameObject.CompareTag("Hitbox"))
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
            audioSource.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
    }
    
}