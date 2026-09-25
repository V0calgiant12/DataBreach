using UnityEngine;
using UnityEngine.UI;

public class HeartCoinIconHandler : MonoBehaviour
{
    
    [SerializeField] private Image image;
    [SerializeField] private PlayerData playerData;
    public static HeartCoinIconHandler Instance;
    void Start()
    {
        Instance = this;
        UpdateGUI();
    }
    public void UpdateGUI()
    {
        image.color = new Color(1,1,1,playerData.hasHeartCoin ? 1:0);
    }
}