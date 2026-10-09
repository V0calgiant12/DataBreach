using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    private ParticleData particleData;
    private Object[] findAssets;
    void Start()
    {
        findAssets = Resources.LoadAll<ParticleData>("ScriptableObjects/Particles");
    }
    public void SpawnParticle(string particleId,Vector2 OverridePositon)
    {
        foreach(ParticleData particle in findAssets)
        {
            if(particle.name == particleId)
            {
                particleData = particle;
            }
        }
        if((particleData.priorityLevel == ParticleData.PriorityLevel.All && SettingsData.Instance._Particles == 0)||(particleData.priorityLevel == ParticleData.PriorityLevel.Decreased && SettingsData.Instance._Particles <= 1)||(particleData.priorityLevel == ParticleData.PriorityLevel.OnlyNecessary && SettingsData.Instance._Particles <= 2))
        {
            Instantiate(particleData.prefab,OverridePositon,transform.rotation);
        }
    }
}