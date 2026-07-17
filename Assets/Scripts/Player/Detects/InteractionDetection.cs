using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionDetection : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private PlayerStateManager player;
    private bool interactable;
    private Collider2D colliderOther;

    void Start()
    {
        indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 0);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 0);
        interactable = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 1);
    }
    void FixedUpdate()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(SettingsData.Instance._InputInteract) && player.playerData.interacting == false)
            {
                InteractableData data = colliderOther.gameObject.GetComponent<InteractableData>();
                indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 0);
                switch (data._Id)
                {
                    case(0):
                        player.Interact();
                        TextWrite.Instance.WriteText(colliderOther.gameObject.GetComponent<TextData>());
                        break;
                }
                
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        colliderOther = other;
        interactable = true;
    }
}