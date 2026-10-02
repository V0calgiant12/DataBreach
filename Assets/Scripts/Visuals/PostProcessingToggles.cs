using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingToggles : MonoBehaviour
{
    [SerializeField] private Volume volume;
    private Bloom bloom;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    void Start()
    {
        volume.profile.TryGet(out bloom);
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out vignette);
        //InvokeRepeating("UpdatePostProcessing",0,1);
    }
    public void UpdatePostProcessing()
    {
        Debug.Log("PostProcessingUpdate");
        bloom.active = SettingsData.Instance._Bloom;
        chromaticAberration.active = SettingsData.Instance._ChromaticAberration;
        vignette.active = SettingsData.Instance._Vignette;
    }
}
