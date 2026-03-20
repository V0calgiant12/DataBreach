using UnityEngine;
using TMPro;
using UnityEditor;
using System.Drawing;
using System.Numerics;

[ExecuteAlways]

public class DivideWidth : MonoBehaviour
{
    [SerializeField] private RectTransform RectFrom;
    [SerializeField] private RectTransform RectTo;
    [SerializeField] private float Padding;
    #if UNITY_EDITOR
    private void Update() {
        if(Padding > 0)
        {
            RectTo.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,RectFrom.rect.width / Padding);
        }
    }
    #else
    private void OnGUI() {
        if(Padding > 0)
        {
            RectTo.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,RectFrom.rect.width / Padding);
        }
    }

    #endif
}
