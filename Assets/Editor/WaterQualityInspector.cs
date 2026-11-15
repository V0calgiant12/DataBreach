#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(WaterQualityController))]
public class WaterQualityInspector : Editor
{
    WaterQualityController script;
    void OnEnable()
    {
        script = (WaterQualityController)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty qualityToGet = serializedObject.FindProperty("qualityToGet");
        SerializedProperty _Function = serializedObject.FindProperty("_Function");
        SerializedProperty settingEnabled = serializedObject.FindProperty("settingEnabled");
        SerializedProperty objects = serializedObject.FindProperty("objects");
        SerializedProperty shaderHQ = serializedObject.FindProperty("shaderHQ");
        SerializedProperty shaderLQ = serializedObject.FindProperty("shaderLQ");
        
        
        EditorGUILayout.PropertyField(_Function);
        EditorGUILayout.PropertyField(qualityToGet);
        EditorGUILayout.PropertyField(settingEnabled);

        if (script._Function == WaterQualityController.Function.ChangeShader)
        {
            EditorGUILayout.PropertyField(shaderHQ);
            EditorGUILayout.PropertyField(shaderLQ);
        }
        else
        {
            EditorGUILayout.PropertyField(objects);
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif