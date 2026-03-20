using UnityEngine;

public class VideoSettingsController : MonoBehaviour
{
    [SerializeField] private GameObject videoMenu;
    public int _Fullscreen;
    
    
    void Start() // Refreshes settings on load.
    {
        videoMenu.SetActive(true);
        RefreshSettings();
        videoMenu.SetActive(false);
    }
    public void SetFullscreenMode(int mode)
    {
        switch(mode){
            case(0):
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case(1):
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; 
                break;
            case(2):
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case(3):
                Screen.fullScreenMode = FullScreenMode.Windowed; 
                break;
        }
    }
    
    public void DropdownSetting(SettingDropdownData data)
    {
        switch (data._DropdownID)
        {
            case(0):
                _Fullscreen = data.dropdown.value;
                SetFullscreenMode(data.dropdown.value);
                break;
        }
    }
    private void RefreshSettings() // Gets the saved settings and tells all other setting objects with the tag "GameMenu" to refresh their visuals, which is handled elsewhere.
    {
        GameObject[] videoMenuItems = GameObject.FindGameObjectsWithTag("VideoMenu"); // Puts all game menu objects in a list.
        int index = 0;
        while (index <= videoMenuItems.Length - 1) // Repeats for every game object.
        {
            videoMenuItems[index].SendMessage("RefreshVisuals");
            index += 1;
        }
    }
}
