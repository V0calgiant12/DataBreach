using System.Numerics;
using UnityEngine;

public class InteractionDetection : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer indicator;

    void Start()
    {
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 1);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(SettingsData.Instance._InputInteract))
        { 
            InteractableData data = other.gameObject.GetComponent<InteractableData>();
            Debug.Log(data._Id);
            switch (data._Id)
            {
                case(0):
                    break;
            }
            
        }
    }
}