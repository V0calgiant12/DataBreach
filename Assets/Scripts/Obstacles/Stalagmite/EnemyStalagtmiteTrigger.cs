using System;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class EnemyStalagtiteTrigger : MonoBehaviour
{
    [Header("Stalagtite References:")]
    public Rigidbody2D StalagmiteRb;
    public GameObject Collider;
    public GameObject Hitbox;
    public GameObject Trigger;
    [SerializeField] private AudioClip _StalactiteGround;
    [SerializeField] private AudioClip _StalactiteDetach;
    [SerializeField] private AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("EnemyHurtbox") && gameObject.CompareTag("Hitbox"))
        {
            other.GetComponent<EnemyHit>().DamageEnemy(10, 10, UnityEngine.Random.Range(6,10),transform.position.x,1);
        }
        if((other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Stone")) && gameObject.CompareTag("Hitbox"))
        {
            audioSource.clip = _StalactiteGround;
            audioSource.Play();
            Hitbox.SetActive(false);
            Collider.SetActive(true);
            StalagmiteRb.bodyType = RigidbodyType2D.Static;
        }
        if(other.gameObject.CompareTag("EnemyHurtbox") && !gameObject.CompareTag("Hitbox"))
        {
            StalagmiteRb.bodyType = RigidbodyType2D.Dynamic;
            Trigger.SetActive(false);
            Collider.SetActive(false);
            Hitbox.SetActive(true);
            audioSource.pitch = UnityEngine.Random.Range(0.6f, 1.2f);
            audioSource.clip = _StalactiteDetach;
            audioSource.Play();
        }
    }

}