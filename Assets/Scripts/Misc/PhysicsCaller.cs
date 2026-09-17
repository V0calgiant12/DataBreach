using System.Collections;
using UnityEngine;

public class PhysicsCaller : MonoBehaviour
{
    public static PhysicsCaller Instance; 
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(PhysicsUpdate());
    }
    private IEnumerator PhysicsUpdate()
    {
        while (1 < 2)
        {
            Debug.Log("Physics Update");
            Physics2D.Simulate(Time.deltaTime);
            yield return null;
        }
    }
}