using UnityEditor.EditorTools;
using UnityEngine;

public class VineUpdater : MonoBehaviour
{
    public GameObject LinkedObjectTop;
    public GameObject LinkedObjectBottom;
    [Tooltip("The file path to the texture used for the vines. Must have 3 parts to the texture.")]
    public string texturePath = "Objects/Vines";
    public int sectionCount;
    public float updateOffset;
    void OnEnable()
    {
        CancelInvoke("OffsetUpdate");
        InvokeRepeating("OffsetUpdate", updateOffset + (sectionCount != 0 ? sectionCount/100 : 0),0.05f);
    }
    void OffsetUpdate()
    {
        if (LinkedObjectBottom == null)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(texturePath)[2];
        }
        if(LinkedObjectTop == null)
        {
            GetComponent<EnemyHit>().DamageEnemy(1,0,0,0);
        }
    }
}