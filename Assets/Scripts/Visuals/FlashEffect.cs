using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Material litMat;
    [SerializeField] private Material hitMat;
    [SerializeField] private Material invulMat;
    private bool whiteFlashing = false;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = litMat;
    }
    public void WhiteFlash()
    {
        StartCoroutine(WhiteFlashAnimation(0.25f));
    }
    public void InvulnerableFlash(float iFrames)
    {
        StartCoroutine(InvulnerableFlashAnimation(iFrames));
    }
    public void SetInvisible(float iFrames)
    {
        StartCoroutine(InvisibleFor(iFrames));
    }

    IEnumerator WhiteFlashAnimation(float time)
    {
        sr.material = hitMat;
        float elapsed = 0;
        whiteFlashing = true;
        while (time > elapsed)
        {
            //sr.color = new UnityEngine.Color(1,1,1,1);
            elapsed += Time.deltaTime;
            yield return null;
        }
        whiteFlashing = false;
        sr.material = litMat;
    }
    IEnumerator InvulnerableFlashAnimation(float time)
    {
        float elapsed = 0;
        float visible = 0;
        UnityEngine.Color originalColor = sr.color;
        while (time > elapsed)
        {
            //sr.color = new UnityEngine.Color(1,1,1,(visible < 0.84f) ? 1 : 0);
            sr.material = (visible < 0.12f) ? invulMat : whiteFlashing ? hitMat : litMat;
            elapsed += Time.deltaTime;
            if(visible >= 0.24f)
            {
                visible = 0;
            }
            visible += Time.deltaTime;
            yield return null;
        }
        //sr.color = new UnityEngine.Color(originalColor.r,originalColor.b,originalColor.g,1);
        sr.material = litMat;
    }
    IEnumerator InvisibleFor(float time)
    {
        float elapsed = 0;
        while (time > elapsed)
        {
            sr.material = invulMat;
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.material = litMat;
    }
}