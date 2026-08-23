using UnityEngine;

public class AdvancedParticleCollision : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    private int collisionCD = 0;
    void Update()
    {
        collisionCD--;
    }
    void OnParticleCollision(GameObject other)
    {
        if (collisionCD < 0)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
            Debug.Log(particles[0].position);
            audioSource.PlaySound(audioClip[Random.Range(0,audioClip.Length-1)], 1f,1f,1f,0.75f,particles[0].position);
            collisionCD = 10;
        }
    }
}