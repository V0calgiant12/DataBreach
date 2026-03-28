using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionDetection : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer indicator;
    [SerializeField] private PlayerStateManager player;

    void Start()
    {
        indicator.color = new Color(indicator.color.r,indicator.color.g,indicator.color.b, 0);
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
        if (Input.GetKeyDown(SettingsData.Instance._InputInteract) && player.playerData.interacting == false)
        {
            InteractableData data = other.gameObject.GetComponent<InteractableData>();
            Debug.Log(data._Id);
            switch (data._Id)
            {
                case(0):
                    player.Intereact();
                    TextWrite.Instance.textBox.Open();
                    TextWrite.Instance.WriteText(other.gameObject.GetComponent<TextData>());
                    break;
            }
            
        }
    }
}