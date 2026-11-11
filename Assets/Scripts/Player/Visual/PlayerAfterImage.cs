using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteAlways]
public class PlayerAfterImage : MonoBehaviour
{
    [SerializeField] private VisualEffect particle;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponentInParent<SpriteRenderer>();
    }
    
    void Update()
    {
        //particle.SetVector2("_Scale",new Vector2(sr.sprite.texture.width,sr.sprite.texture.height));
        //particle.SetVector2("_Pivot",new Vector2(sr.sprite.pivot.x/sr.sprite.texture.width,sr.sprite.pivot.y/sr.sprite.texture.height));
        particle.SetFloat("_Flip",transform.lossyScale.x);
        particle.SetFloat("_Rotation",transform.rotation.eulerAngles.z);
        particle.SetVector4("_SpriteColor",PlayerColor.rainbowColor);
    }
}