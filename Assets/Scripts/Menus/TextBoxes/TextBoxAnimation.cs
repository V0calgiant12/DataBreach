using System.Collections;
using UnityEngine;

public class TextBoxAnimation : MonoBehaviour 
{
    //Vector2(0.023,0.0399999991)
    //Vector2(0.976999998,0.349999994)
    [Header("Peramiters")]
    public float _OpenSpeed;
    [SerializeField] private bool open = false;
    [Header("References")]
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Animator animator;
    void Start()
    {
        rectTransform.ForceUpdateRectTransforms();
    }
    public void Open()
    {
        animator.SetFloat("Speed", _OpenSpeed);
        animator.SetBool("Open", true);
        open = true;
    }
    public void Close()
    {
        animator.SetFloat("Speed", _OpenSpeed);
        animator.SetBool("Open", false);
        open = false;
    }
}