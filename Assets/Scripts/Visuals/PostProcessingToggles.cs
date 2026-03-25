using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingToggles : MonoBehaviour
{
    [SerializeField] private Volume volume;
    void Start()
    {
        UpdatePostProcessing();
    }
    public void UpdatePostProcessing()
    {
        if(volume.profile.TryGet(out Bloom bloom))
        {
            if (SettingsData.Instance._Bloom)
            {
                bloom.active = true;
            }
            else
            {
                bloom.active = false;
            }
        }
        if(volume.profile.TryGet(out ChromaticAberration chromaticAberration))
        {
            if (SettingsData.Instance._Bloom)
            {
                chromaticAberration.active = true;
            }
            else
            {
                chromaticAberration.active = false;
            }
        }
        if(volume.profile.TryGet(out Vignette vignette))
        {
            if (SettingsData.Instance._Bloom)
            {
                vignette.active = true;
            }
            else
            {
                vignette.active = false;
            }
        }
    }
}
