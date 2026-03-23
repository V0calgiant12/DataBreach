using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class ShaderToggle : MonoBehaviour
{
    [SerializeField] private Material shaderMaterial;
    [SerializeField] private RenderFeatureToggler renderFeatureToggler;
    public GlobalKeyword pixelShaderDisabled;
    void Start()
    {
        pixelShaderDisabled = GlobalKeyword.Create("_ENABLED");
        if (SettingsData.Instance._Pixelation)
        {
            Debug.Log("Shader On");
            renderFeatureToggler.EnableRenderFeatures();
            Shader.DisableKeyword(pixelShaderDisabled);
        }
        else
        {
            Debug.Log("Shader Off");
            renderFeatureToggler.DisableRenderFeatures();
            Shader.EnableKeyword(pixelShaderDisabled);
        }
    }
}