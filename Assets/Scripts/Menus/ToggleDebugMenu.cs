using UnityEngine;

public class ToggleDebugMenu : MonoBehaviour
{
    public static ToggleDebugMenu Instance;
    [SerializeField] private GameObject debugMenu;
    private bool debugActive = false;
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
        debugActive = false;
        debugMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            debugActive = !debugActive;
            debugMenu.SetActive(debugActive);
        }
    }
}