using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SetTextBackground : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        InvokeRepeating("OffsetUpdate",0,1);
    }
    private void OffsetUpdate()
    {
        sr.size = new Vector2(text.text.Length + (text.text.Length < 3 ? text.text.Length < 2 ? 0.5f : 1 :text.text.Length > 3 ? -1 : 0),1.5f);
    }
}