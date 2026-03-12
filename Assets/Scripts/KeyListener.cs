using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour
{
    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log(e.keyCode);
        }
    }
}
