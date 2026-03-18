using UnityEngine;
using TMPro;
using UnityEditor;
using System.Drawing;
using System.Numerics;

[ExecuteAlways]

public class CopyWidth : MonoBehaviour
{
    [SerializeField] private RectTransform RectFrom;
    [SerializeField] private RectTransform RectTo;
    [SerializeField] private float Padding;
    #if UNITY_EDITOR
    private void Update() {
        RectTo.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,RectFrom.rect.width - Padding);
    }
    #else
    private void OnGUI() {
        RectTo.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,RectFrom.rect.width - Padding);
    }

    #endif
}
