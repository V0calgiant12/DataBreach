using UnityEngine;

public class ParticleCollisionAudio : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip landSound;
    private int collisions = 0;
    void OnParticleCollision(GameObject other)
    {
        if (collisions < 10)
        {
            audioSource.PlaySound(landSound, 0.15f, Random.Range(1.5f,1.8f), 0f, 1, transform.position);
            collisions += 1;
        }
    }
}