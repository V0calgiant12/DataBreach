using Unity.VisualScripting;
using UnityEngine;

public class LightingAreaTrigger : MonoBehaviour
{
    [SerializeField] private bool detectPlayer = false;
    [SerializeField] private int activeLightId = 0;
    [SerializeField] private LightingAreaHolder holder;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("RealCamera") && !detectPlayer)
        {
            Enter(other);
        }
        else if(other.gameObject.CompareTag("Player") && detectPlayer)
        {
            Enter(other);
        }
    }
    private void Enter(Collider2D other)
    {
        Debug.Log("Entered Lighing Area",this);
        for(int i = 0; i < holder.lightingObjects.Length; i++)
        {
            if(holder.lightingObjects[i].GetComponent<LightingAreaObject>().id != activeLightId)
            {
                holder.lightingObjects[i].SetActive(false);
            }
            else
            {
                holder.lightingObjects[i].SetActive(true);
            }
        }
        
    }
}