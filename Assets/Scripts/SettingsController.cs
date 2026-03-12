using UnityEngine;
using System.Collections;

public class SettingsController : MonoBehaviour
{
    private string currentKeyDown;
    public IEnumerator StartListeningForKey()
    {
        return wait
    } 
    
    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            currentKeyDown = e.keyCode;
            Debug.Log(e.keyCode);
        }
    }
}
