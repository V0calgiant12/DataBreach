using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextData))]
public class TextDataInspector : Editor
{
    TextData script;
    void OnEnable()
    {
        script = (TextData)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SerializedProperty _TextPageInput = serializedObject.FindProperty("_TextPageInput");
        SerializedProperty _TextSpeed = serializedObject.FindProperty("_TextSpeed");
        SerializedProperty _DelayBetweenLines = serializedObject.FindProperty("_DelayBetweenLines");
        SerializedProperty _DecisionAfterText = serializedObject.FindProperty("_DecisionAfterText");
        SerializedProperty _DecisionOptions = serializedObject.FindProperty("_DecisionOptions");
        SerializedProperty _UsesHeartCoin = serializedObject.FindProperty("_UsesHeartCoin");
        SerializedProperty _PostDecisionTextDecision1 = serializedObject.FindProperty("_PostDecisionTextDecision1");
        SerializedProperty _PostDecisionTextDecision2 = serializedObject.FindProperty("_PostDecisionTextDecision2");
        SerializedProperty _TextSound = serializedObject.FindProperty("_TextSound");
        
        
        EditorGUILayout.PropertyField(_TextPageInput);
        EditorGUILayout.PropertyField(_TextSpeed);
        EditorGUILayout.PropertyField(_DelayBetweenLines);
        EditorGUILayout.PropertyField(_DecisionAfterText);

        if (script._DecisionAfterText)
        {
            EditorGUILayout.PropertyField(_UsesHeartCoin);
            EditorGUILayout.PropertyField(_DecisionOptions);
            EditorGUILayout.PropertyField(_PostDecisionTextDecision1);
            EditorGUILayout.PropertyField(_PostDecisionTextDecision2);
        }
        EditorGUILayout.PropertyField(_TextSound);
        serializedObject.ApplyModifiedProperties();
    }
}