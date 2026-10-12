using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VineCreator))]
public class VineCreatorInspector : Editor
{
    VineCreator script;
    void Awake()
    {
        script = (VineCreator)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(GUILayout.Button("Refresh Vines"))
        {
            script.RefreshVines();
        }
    }
}