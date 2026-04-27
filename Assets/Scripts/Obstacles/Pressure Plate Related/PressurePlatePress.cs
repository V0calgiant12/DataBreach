using UnityEngine;

public class PressurePlatePress : MonoBehaviour
{
    //change Object1 and Object to things that the pressure plate activate
    public GameObject Object;
    public GameObject Object1;
    //public DartShooter ObjectRef;
    //public Boulder Object1Ref;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected by pressure plate");
            //Make sure to use the right tag for the object(s) you are using
            if(transform.parent.CompareTag("Object"))
            {
                //call a function from the object you are activating
                //ObjectRef.ShootDarts();
                Debug.Log("Dart Shooter");
            }
            //Make sure to use the right tag for the object(s) you are using
            if(transform.parent.CompareTag("Object1"))
            {
                //call a function from the object you are activating
                //Object1Ref.RollBoulder();
                Debug.Log("Boulder");
            }
            
        }
    }
}