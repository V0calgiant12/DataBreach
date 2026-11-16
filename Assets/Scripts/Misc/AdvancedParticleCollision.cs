using System.Collections.Generic;
using UnityEngine;

public class AdvancedParticleCollision : MonoBehaviour
{
    [SerializeField] private EffectSound audioSource;
    [SerializeField] private AudioClip[] audioClip;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Vector2 pitch = new Vector2(1,1);
    [SerializeField] private float soundCd = 0.1f;
    private ParticleSystem.Particle[] particles;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private float collisionCD = 0;
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    void Update()
    {
        collisionCD -= Time.deltaTime;
    }
    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(ps,other,collisionEvents);
        if (collisionCD <= 0)
        {
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
            ps.GetParticles(particles);
            for(int i = 0; i < collisionEvents.Count; i++)
            {
                audioSource.PlaySound(audioClip[Random.Range(0,audioClip.Length-1)],1f,Random.Range(pitch.x,pitch.y),1f,0.75f,collisionEvents[i].intersection);
            }
            collisionCD = soundCd;
        }
    }
}