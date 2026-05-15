using System.Collections;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private Material litMat;
    [SerializeField] private Material hitMat;
    [SerializeField] private Material invulMat;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.material = litMat;
    }
    public void WhiteFlash()
    {
        StartCoroutine(WhiteFlashAnimation(15));
    }
    public void InvulnerableFlash(int iFrames)
    {
        StartCoroutine(InvulnerableFlashAnimation(iFrames));
    }

    IEnumerator WhiteFlashAnimation(int time)
    {
        sr.material = hitMat;
        int elapsed = 0;
        while (time > elapsed)
        {
            //sr.color = new UnityEngine.Color(1,1,1,1);
            elapsed += 1;
            yield return null;
        }
        sr.material = litMat;
    }
    IEnumerator InvulnerableFlashAnimation(int time)
    {
        int elapsed = 0;
        int visible = 0;
        UnityEngine.Color originalColor = sr.color;
        while (time > elapsed)
        {
            //sr.color = new UnityEngine.Color(1,1,1,(visible < 5) ? 1 : 0);
            sr.material = (visible < 5) ? invulMat : (elapsed < 15) ? hitMat : litMat;
            elapsed += 1;
            visible += 1;
            if(visible == 10)
            {
                visible = 0;
            }
            yield return null;
        }
        //sr.color = new UnityEngine.Color(originalColor.r,originalColor.b,originalColor.g,1);
        sr.material = litMat;
    }
}