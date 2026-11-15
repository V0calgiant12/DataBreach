using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterQualityController : MonoBehaviour
{
    [Header("Settings:")]
    public Function _Function;
    public enum Function
    {
        DisableObjects,
        ChangeShader
    }
    [SerializeField] private Quality qualityToGet;
    private enum Quality
    {
        Low,
        Medium,
        High
    }
    [SerializeField] private bool settingEnabled;
    [Header("Disable Objects:")]
    [SerializeField] private GameObject[] objects;
    private TilemapRenderer tmr;
    [Header("Shader Inputs:")]
    [SerializeField] private Material shaderHQ;
    [SerializeField] private Material shaderLQ;
    void Start()
    {
        tmr = GetComponent<TilemapRenderer>();
        OffsetUpdate();
    }
    void OnEnable()
    {
        CancelInvoke("OffsetUpdate");
        InvokeRepeating("OffsetUpdate",Random.Range(0.1f,0.5f),0.5f);
    }
    private void OffsetUpdate()
    {
        switch (_Function)
        {
            case(Function.DisableObjects):
                settingEnabled = GetSetting(qualityToGet);
                for(int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(settingEnabled);
                }
                return;
            case(Function.ChangeShader):
                settingEnabled = GetSetting(qualityToGet);
                if (settingEnabled)
                {
                    tmr.material = shaderHQ;
                }
                else
                {
                    tmr.material = shaderLQ;
                }
                return;
        }
    }
    private bool GetSetting(Quality quality)
    {
        switch (quality)
        {
            case(Quality.Low):
                if (SettingsData.Instance._WaterQuality <= 2)
                {
                    return true;
                }
                return false;
            case(Quality.Medium):
                if (SettingsData.Instance._WaterQuality <= 1)
                {
                    return true;
                }
                return false;
            case(Quality.High):
                if (SettingsData.Instance._WaterQuality == 0)
                {
                    return true;
                }
                return false;
        }
        return false;
    }
}