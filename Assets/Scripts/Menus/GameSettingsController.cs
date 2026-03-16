using UnityEngine;

public class GameSettingsController : MonoBehaviour
{
    [Header("Toggles")]
    public bool _RunInBackground = true;

    void Start() // Refreshes settings on load.
    {
        RefreshSettings();
    }
    public void ToggleSetting(SettingsToggleData data)
    {
        switch(data._ToggleID)
        {
            //case 0 taken by Up To Jump in controls menu
            case(1): // Run in background
                _RunInBackground = !_RunInBackground;
                UnityEditor.PlayerSettings.runInBackground = !UnityEditor.PlayerSettings.runInBackground;
                break;
        }
    }
    private void RefreshSettings() // Gets the saved settings and tells all other setting objects with the tag "GameMenu" to refresh their visuals, which is handled elsewhere.
    {
        _RunInBackground = SettingsData.Instance._RunInBackground;

        GameObject[] controlsMenuItems = GameObject.FindGameObjectsWithTag("GameMenu"); // Puts all controls menu objects in a list.
        int index = 0;
        while (index <= controlsMenuItems.Length - 1) // Repeats for every game object.
        {
            controlsMenuItems[index].SendMessage("RefreshVisuals");
            index += 1;
        }
    }
}
