using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class KeyboardNavigation : MonoBehaviour
{
    [SerializeField] private Button DefaultSelected;
    [SerializeField] private Button currentlySelected;
    void Start()
    {
        currentlySelected = DefaultSelected;
        currentlySelected.Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(SettingsData.Instance._InputDown))
        {
            currentlySelected = currentlySelected.FindSelectableOnDown().GetComponent<Button>();
            currentlySelected.Select();
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputUp))
        {
            currentlySelected = currentlySelected.FindSelectableOnUp().GetComponent<Button>();
            currentlySelected.Select();
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputLeft))
        {
            currentlySelected = currentlySelected.FindSelectableOnLeft().GetComponent<Button>();
            currentlySelected.Select();
        }
        if (Input.GetKeyDown(SettingsData.Instance._InputRight))
        {
            currentlySelected = currentlySelected.FindSelectableOnRight().GetComponent<Button>();
            currentlySelected.Select();
        }
    }
}