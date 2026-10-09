using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParticleEnabler : MonoBehaviour
{
    [SerializeField] private string particleId;
    [SerializeField] private bool settingEnabled;
    [SerializeField] private bool flipOutput;
    [SerializeField] private bool state;
    [SerializeField] private GameObject[] objects;
    private Object[] findAssets;
    private ParticleData particleData;
    void Start()
    {
        findAssets = Resources.LoadAll<ParticleData>("ScriptableObjects/Particles");
        OffsetUpdate();
    }
    void OnEnable()
    {
        CancelInvoke("OffsetUpdate");
        InvokeRepeating("OffsetUpdate",0.1f + Random.Range(0.0f,0.25f),1f);
    }
    private void OffsetUpdate()
    {
        settingEnabled = GetPriorityLevel();
        if (flipOutput)
        {
            settingEnabled = !settingEnabled;
        }
        for(int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(settingEnabled);
        }
    }
    private bool GetPriorityLevel()
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
            return true;
        }
        return false;
    }
}