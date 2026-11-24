using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TestScript : MonoBehaviour
{
    [SerializeField] private UniversalRenderPipelineAsset urd;
    [SerializeField] private Renderer2DData _2DRenderer;
    void Start()
    {
        urd.LoadBuiltinRendererData(RendererType._2DRenderer);

    }
}