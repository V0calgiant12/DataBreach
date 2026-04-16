using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEditor;

[ExecuteAlways]
public class SetNegativeLighting : MonoBehaviour
{
    [SerializeField] private Light2D LightSource;
    public float _Intesity;
    void Start()
    {
        LightSource.intensity = _Intesity;
    }
    #if UNITY_EDITOR
    private void Update()
    {
        LightSource.intensity = _Intesity;
    }
    #endif
}
