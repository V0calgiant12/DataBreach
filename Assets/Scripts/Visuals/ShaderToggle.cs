using UnityEngine;
using UnityEngine.Rendering;

public class ShaderToggle : MonoBehaviour
{
    [SerializeField] private Material shaderMaterial;
    public GlobalKeyword pixelShaderDisabled;
    void Start()
    {
        pixelShaderDisabled = GlobalKeyword.Create("_ENABLED");
        if (SettingsData.Instance._Pixelation)
        {
            Debug.Log("Shader On");
            Shader.DisableKeyword(pixelShaderDisabled);
        }
        else
        {
            Debug.Log("Shader Off");
            Shader.EnableKeyword(pixelShaderDisabled);
        }
    }
}
