using System;
using UnityEngine;

[ExecuteAlways]
public class VineCreator : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [Header("Settings")]
    public int vineLength = 3;
    private string vinePath;
    public void RefreshVines()
    {
        vinePath = prefab.GetComponent<VineUpdater>().texturePath;
        RemoveVines();
        CreateVines();
    }
    void RemoveVines()
    {
        GameObject[] vines = null;
        Array.Resize(ref vines, transform.childCount);
        for(int i = 0; i < transform.childCount; i++)
        {
            vines[i] = transform.GetChild(i).gameObject;
        }
        foreach(GameObject vine in vines)
        {
            DestroyImmediate(vine);
        }
    }
    void CreateVines()
    {
        GameObject[] vines = null;
        Array.Resize(ref vines, vineLength);
        for(int i = 0; i < vineLength; i++)
        {
            Instantiate(prefab,new Vector2(transform.position.x,transform.position.y-i-0.5f),transform.rotation).transform.SetParent(transform);
            vines[i] = transform.GetChild(i).gameObject;
            if(i != 0)
            {
                // Middle of Vine
                vines[i].name = "VineMiddle";
                vines[i].GetComponent<HingeJoint2D>().connectedBody = vines[i-1].GetComponent<Rigidbody2D>();
                vines[i].GetComponent<VineUpdater>().LinkedObjectTop = vines[i-1];
                vines[i-1].GetComponent<VineUpdater>().LinkedObjectBottom = vines[i];
            }
            else
            {
                // Top of Vine
                vines[i].name = "VineTop";
                vines[i].GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(vinePath)[0];
                vines[i].GetComponent<HingeJoint2D>().autoConfigureConnectedAnchor = true;
                vines[i].GetComponent<VineUpdater>().LinkedObjectTop = vines[i];
                BoxCollider2D collider = vines[i].GetComponent<BoxCollider2D>();
                collider.offset = new Vector2(0,-0.25f);
                collider.size = new Vector2(0.3f,0.5f);
            }
            if(i == vineLength-1)
            {
                // End of Vine
                vines[i].name = "VineEnd";
                vines[i].GetComponent<SpriteRenderer>().sprite = Resources.LoadAll<Sprite>(vinePath)[2];
                vines[i].GetComponent<VineUpdater>().LinkedObjectTop = vines[i-1];
                vines[i].GetComponent<VineUpdater>().LinkedObjectBottom = vines[i];
            }
            vines[i].GetComponent<VineUpdater>().sectionCount = i;
        }
        foreach(GameObject vine in vines)
        {
            
        }
    }
}