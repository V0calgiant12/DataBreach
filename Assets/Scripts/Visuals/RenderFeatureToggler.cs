using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public struct RenderFeatureToggle
{
    public ScriptableRendererFeature feature;
}

[ExecuteAlways]
public class RenderFeatureToggler : MonoBehaviour
{
    [SerializeField] private List<RenderFeatureToggle> renderFeatures = new List<RenderFeatureToggle>();
    [SerializeField] private UniversalRenderPipelineAsset pipelineAsset;

    public void EnableRenderFeatures()
    {
        foreach (RenderFeatureToggle toggleObj in renderFeatures)
        {
            toggleObj.feature.SetActive(true);
        }
    }
    public void DisableRenderFeatures()
    {
        foreach (RenderFeatureToggle toggleObj in renderFeatures)
        {
            toggleObj.feature.SetActive(false);
        }
    }
}