using System;
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
            PlayerStateManager.Instance.DamagePlayer(10, UnityEngine.Random.Range(6,10),60);
            Debug.Log("Stalagtite Damaged Player " + Convert.ToInt16(PlayerStateManager.Instance.playerData.leftOrRight));
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
            audioSource.pitch = UnityEngine.Random.Range(0.6f, 1.2f);
            audioSource.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
    }
    
}