using Unity.VisualScripting;
using UnityEngine;

public class RainbowParticleEnabler : MonoBehaviour
{
    [SerializeField] private GameObject target;
    void Update()
    {
        if (PlayerColor.rainbow)
        {
            target.SetActive(true);
        }
        else
        {
            target.SetActive(false);
        }
    }
}