using UnityEngine;

public class SaveGameOnEnter : MonoBehaviour
{
    void Awake()
    {
        GameData.Instance.SaveData();
        GameObject.Find("Screen").GetComponent<Animator>().SetBool("IsSaveScene", true);
    }
}
