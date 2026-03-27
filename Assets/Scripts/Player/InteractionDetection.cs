using System.Numerics;
using UnityEngine;

public class InteractionDetection : MonoBehaviour 
{
    [SerializeField] private GameObject indicator;

    void Start()
    {
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        indicator.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        indicator.SetActive(true);
        if (Input.GetKeyDown(SettingsData.Instance._InputInteract))
        {
            InteractableData data = other.gameObject.GetComponent<InteractableData>();
            indicator.transform.localPosition = new UnityEngine.Vector2(0,indicator.transform.localPosition.y + 1);
            Debug.Log(data._Id);
            switch (data._Id)
            {
                case(0):
                    break;
            }
            
        }
    }
}