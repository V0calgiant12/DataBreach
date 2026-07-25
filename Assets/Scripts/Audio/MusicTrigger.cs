using Unity.VisualScripting;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    [SerializeField] private MusicManager musicManager;
    [SerializeField] private int instrumentCount;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            musicManager.ChangeInstruments(instrumentCount);
        }
    }
}