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
            audioSource.PlaySound(landSound, 0.1f,Random.Range(0.9f,1.1f),0.5f);
            collisions += 1;
        }
    }
}