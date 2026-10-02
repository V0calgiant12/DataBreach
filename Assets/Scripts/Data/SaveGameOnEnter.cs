using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameOnEnter : MonoBehaviour
{
    void Start()
    {
        GameData.Instance._SceneId = SceneManager.GetActiveScene().buildIndex;
        GameObject.Find("Screen").GetComponent<Animator>().SetTrigger("Save");
        GameData.Instance.SaveData();
    }
}
