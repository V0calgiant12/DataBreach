using System;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommandMenu : MonoBehaviour
{
    private bool commandPromptOpen = false;
    [SerializeField] private GameObject CommandConsole;
    [SerializeField] private TMP_InputField commandPrompt;
    [SerializeField] private TextMeshProUGUI log;
    [SerializeField] private PlayerData playerData;
    void Start()
    {
        CommandConsole.SetActive(false);
        commandPromptOpen = false;
    }
    void Update()
    {
        if (!commandPromptOpen && Input.GetKeyDown(KeyCode.Slash))
        {
            OpenCommandPrompt();
        }
        if (commandPromptOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCommandPrompt();
        }
        if (log.isTextTruncated)
        {
            while(char.ToString(log.text[0]) != ">")
            {
                log.text = log.text.Substring(1);
            }
            log.text = log.text.Substring(1);
        }
    }
    public void OpenCommandPrompt()
    {
        UserInput.Instance._playerInput.actions.Disable();
        commandPromptOpen = true;
        CommandConsole.SetActive(true);
        commandPrompt.Select();
        commandPrompt.ActivateInputField();
    }
    public void CloseCommandPrompt()
    {
        commandPromptOpen = false;
        UserInput.Instance._playerInput.actions.Enable();
        CommandConsole.SetActive(false);
    }
    public void SubmitCommand(string commandInput)
    {
        commandPrompt.Select();
        commandPrompt.ActivateInputField();
        string[] commandSplit = commandInput.ToLower().Split();
        Array.Resize(ref commandSplit,commandSplit.Length + 5); 
        commandPrompt.text = "";
        int id = -1;
        int number = -1;
        float x = 0;
        float y = 0;
        switch (commandSplit[0])
        {
            case("save"):
                GameData.Instance._SceneId = SceneManager.GetActiveScene().buildIndex;
                GameData.Instance.SaveData();
                GameObject.Find("Screen").GetComponent<Animator>().SetTrigger("Save");
                LogOutput("Saved Game");
                return;
            case("loadscene"):
                SceneTransition sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
                Int32.TryParse(commandSplit[1], out id);
                if(id >= 0 && id <= SceneManager.sceneCountInBuildSettings)
                {
                    playerData.lastCheckpoint = new Vector2(0,0);
                    sceneTransition.TransitionToScene(id,1);
                    LogOutput("Loading scene with Id " + id);
                }
                else
                {
                    LogOutput("ERROR: Scene Id not provided or invalid");
                }
                return;
            case("sethealth"):
                Int32.TryParse(commandSplit[1], out number);
                if(number >= 0)
                {
                    playerData.playerHealth = number;
                    LogOutput("Set player health to " + number);
                }
                else if(commandSplit[1] == "max")
                {
                    playerData.playerHealth = playerData.maxHealth;
                    LogOutput("Set player health to max.");
                }
                else
                {
                    LogOutput("ERROR: Invalid health amount.");
                }
                return;
            case("setmaxhealth"):
                Int32.TryParse(commandSplit[1], out number);
                if(number > 0 && number < 11)
                {
                    playerData.maxHealth = number;
                    LogOutput("Set max player health to " + number);
                }
                else
                {
                    LogOutput("ERROR: Invalid health amount.");
                }
                return;
            case("setcheckpoint"):
                float.TryParse(commandSplit[1], out x);
                float.TryParse(commandSplit[2], out y);
                if(commandSplit[1] == "~")
                {
                    if(PlayerStateManager.Instance != null)
                    {
                        x = PlayerStateManager.Instance.transform.position.x;
                    }
                    else
                    {
                        LogOutput("ERROR: Player does not currently exist in scene. Please use a float instead of a tilda.");
                    }
                }
                if(commandSplit[2] == "~")
                {
                    if(PlayerStateManager.Instance != null)
                    {
                        y = PlayerStateManager.Instance.transform.position.y;
                    }
                    else
                    {
                        LogOutput("ERROR: Player does not currently exist in scene. Please use a float instead of a tilda.");
                    }
                }
                playerData.lastCheckpoint = new Vector2(x,y);
                LogOutput("Set new checkpoint to " + playerData.lastCheckpoint);
                return;
        }
        if(commandInput != "")
        {
            LogOutput("ERROR: Command invalid or not found.");
        }
    }
    public void LogOutput(string logString)
    {
        if(log.text != "")
        {
            log.text += "<br>" + logString;
        }
        else
        {
            log.text += logString;
        }
        Debug.Log("Command Output: \"" + logString + "\"");
    }
}