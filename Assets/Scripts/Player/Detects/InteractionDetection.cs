using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InteractionDetection : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private Light2D ambientLight;
    [SerializeField] private PlayerStateManager player;
    private bool interactable;
    private Collider2D colliderOther;

    void Start()
    {
        indicator.enabled = false;
        ambientLight.enabled = false;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        indicator.enabled = false;
        ambientLight.enabled = false;
        interactable = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        indicator.enabled = true;
        ambientLight.enabled = true;
    }
    void FixedUpdate()
    {
        if (interactable && player.playerData.interactingCooldown < 0)
        {
            if (UserInput.Instance.KeyDownInteract && player.playerData.interacting == false)
            {
                InteractableData data = colliderOther.gameObject.GetComponent<InteractableData>();
                indicator.enabled = false;
                ambientLight.enabled = false;
                switch (data._TypeId)
                {
                    case(0):
                        player.Interact(PlayerStateManager.InteractControls.Stop,0);
                        TextWrite.Instance.WriteText(colliderOther.gameObject.GetComponent<TextData>());
                        break;
                    case(1):
                        player.Interact(PlayerStateManager.InteractControls.Stop,0);
                        TextData textData = colliderOther.gameObject.GetComponent<TextData>();
                        if (PlayerStateManager.Instance.playerData.hasHeartCoin)
                        {
                            textData.ChangeText(3,"Hey, is that a Heart Coin you got there?",false);
                            textData.ChangeText(4,"I can use that Heart Coin on ya if you'd like.<br>They're easy to lose so it's better to use them while you have them.",false);
                            textData._DecisionAfterText = true;
                        }
                        else
                        {
                            textData.TrimPages(3);
                            textData._DecisionAfterText = false;
                        }
                        TextWrite.Instance.WriteText(textData);

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