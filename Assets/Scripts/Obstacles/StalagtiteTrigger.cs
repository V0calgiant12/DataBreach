using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class StalagtiteTrigger : MonoBehaviour
{
    [Header("Stalagtite References:")]
    public Rigidbody2D StalagmiteRb;
    public GameObject Collider;
    public GameObject Hitbox;
    public GameObject Trigger;
    [SerializeField] private AudioClip _StalactiteGround;
    [SerializeField] private AudioClip _StalactiteDetach;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && gameObject.CompareTag("Hitbox"))
        {
            PlayerStateManager.Instance.DamagePlayer(10, UnityEngine.Random.Range(6,10),60,false);
            Debug.Log("Stalagtite Damaged Player " + Convert.ToInt16(PlayerStateManager.Instance.playerData.leftOrRight));
        }
        if((other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Stone")) && gameObject.CompareTag("Hitbox"))
        {
            audioSource2.Play();
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

}