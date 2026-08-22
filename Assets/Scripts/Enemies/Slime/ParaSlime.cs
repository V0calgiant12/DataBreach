using Unity.VisualScripting;
using UnityEngine;

public class ParaSlime : MonoBehaviour
{
    [SerializeField] private Vector3 nextPos;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        InvokeRepeating(nameof(PlaySound),0.5f,0.5f);
    }
    private void PlaySound()
    {
        audioSource.pitch = Random.Range(0.7f,1.3f);
    }
}