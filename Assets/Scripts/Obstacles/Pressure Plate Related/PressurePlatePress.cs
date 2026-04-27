using UnityEngine;

public class PressurePlatePress : MonoBehaviour
{
    public GameObject DartShooter;
    public GameObject Boulder;
    public DartShooter DartShooterRef;
    public Boulder BoulderRef;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected by pressure plate");
            if(transform.parent.CompareTag("DartShooter"))
            {
                DartShooterRef.ShootDarts();
                Debug.Log("Dart Shooter");
            }
            if(transform.parent.CompareTag("Boulder"))
            {
                BoulderRef.RollBoulder();
                Debug.Log("Boulder");
            }
            
        }
    }
}