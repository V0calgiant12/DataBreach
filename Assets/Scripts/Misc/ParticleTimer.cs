using UnityEngine;

public class ParticleTimer : MonoBehaviour
{
    private int timer;
    void Stat()
    {
        timer == 180;
    }
    private void Update()
    {
        timer -= 1;
        if(timer == 0)
        {
            Destroy(this);
        }
    }
}