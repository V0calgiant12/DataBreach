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
    public string[] CommandList {get;} = {"help","save","loadscene <int>","sethealth <int>","setmaxhealth <int>","setcheckpoint <x> <y>","tp <x> <y>"}; 
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
            case("help"):
                string listOutput = "Command list: "; 
                for(int i = 0; i < CommandList.Length; i++)
                {
                    if(listOutput != "Command list: ")
                    {
                        listOutput += ", ";
                    }
                    listOutput += CommandList[i];
                }
                LogOutput(listOutput);
                return;
            case("save"):
                if(SceneManager.GetActiveScene().buildIndex == 0)
                {
                    LogOutput("ERROR: Cannot save in Main Menu due to missing game objects.");
                    return;
                }
                GameData.Instance._SceneId = SceneManager.GetActiveScene().buildIndex;
                GameData.Instance.SaveData();
                GameObject.Find("Screen").GetComponent<Animator>().SetTrigger("Save");
                LogOutput("Saved Game");
                return;
            case("loadscene"):
                SceneTransition sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
                if(sceneTransition == null)
                {
                    LogOutput("ERROR: No scene transition found.");
                    return;
                }
                if (Int32.TryParse(commandSplit[1], out id) && id >= 0 && id <= SceneManager.sceneCountInBuildSettings)
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
                if(Int32.TryParse(commandSplit[1], out number))
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
                if(Int32.TryParse(commandSplit[1], out number))
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
                if(!float.TryParse(commandSplit[1], out x))
                {
                    if(char.ToString(commandSplit[1][0]) == "~")
                    {
                        if(PlayerStateManager.Instance != null)
                        {
                            x = PlayerStateManager.Instance.transform.position.x;
                            commandSplit[1] = commandSplit[1].Replace("~", string.Empty);
                            if(float.TryParse(commandSplit[1], out float addX))
                            {
                                x += addX;
                            }
                            else if(commandSplit[1] != "")
                            {
                                LogOutput("ERROR: Not a float.");
                                return;
                            }
                        }
                        else
                        {
                            LogOutput("ERROR: Player does not currently exist in scene. Please use a float instead of a tilda.");
                            return;
                        }
                    }
                    else
                    {
                        LogOutput("ERROR: Invalid float. If you are using a ~, please make sure any numbers you want to add go directly after it.");
                        return;
                    }
                }
                if(!float.TryParse(commandSplit[2], out y))
                {
                    if(char.ToString(commandSplit[2][0]) == "~")
                    {
                        if(PlayerStateManager.Instance != null)
                        {
                            y = PlayerStateManager.Instance.transform.position.y;
                            commandSplit[2] = commandSplit[2].Replace("~", string.Empty);
                            if(float.TryParse(commandSplit[2], out float addY))
                            {
                                y += addY;
                            }
                            else if(commandSplit[2] != "")
                            {
                                LogOutput("ERROR: Not a float.");
                                return;
                            }
                        }
                        else
                        {
                            LogOutput("ERROR: Player does not currently exist in scene. Please use a float instead of a tilda.");
                            return;
                        }
                    }
                    else
                    {
                        LogOutput("ERROR: Invalid float. If you are using a ~, please make sure any numbers you want to add go directly after it.");
                        return;
                    }
                }
                playerData.lastCheckpoint = new Vector2(x,y);
                LogOutput("Set new checkpoint to " + playerData.lastCheckpoint);
                return;
            case("tp"):
                if(PlayerStateManager.Instance != null)
                {
                    if(!float.TryParse(commandSplit[1], out x))
                    {
                        if(char.ToString(commandSplit[1][0]) == "~")
                        {
                            if(PlayerStateManager.Instance != null)
                            {
                                x = PlayerStateManager.Instance.transform.position.x;
                                commandSplit[1] = commandSplit[1].Replace("~", string.Empty);
                                if(float.TryParse(commandSplit[1], out float addX))
                                {
                                    x += addX;
                                }
                                else if(commandSplit[1] != "")
                                {
                                    LogOutput("ERROR: Not a float.");
                                    return;
                                }
                            }
                            else
                            {
                                LogOutput("ERROR: Player does not currently exist in scene. Please use a float instead of a tilda.");
                                return;
                            }
                        }
                        else
                        {
                            LogOutput("ERROR: Invalid float. If you are using a ~, please make sure any numbers you want to add go directly after it.");
                            return;
                        }
                    }
                    if(!float.TryParse(commandSplit[2], out y))
                    {
                        if(char.ToString(commandSplit[2][0]) == "~")
                        {
                            if(PlayerStateManager.Instance != null)
                            {
                                y = PlayerStateManager.Instance.transform.position.y;
                                commandSplit[2] = commandSplit[2].Replace("~", string.Empty);
                                if(float.TryParse(commandSplit[2], out float addY))
                                {
                                    y += addY;
                                }
                                else if(commandSplit[2] != "")
                                {
                                    LogOutput("ERROR: Not a float.");
                                    return;
                                }
                            }
                            else
                            {
                                LogOutput("ERROR: Player does not currently exist in scene. Please use a float instead of a tilda.");
                                return;
                            }
                        }
                        else
                        {
                            LogOutput("ERROR: Invalid float. If you are using a ~, please make sure any numbers you want to add go directly after it.");
                            return;
                        }
                    }
                    PlayerStateManager.Instance.transform.position = new Vector2(x,y);
                    playerData.PlayerRb.position = new Vector2(x,y);
                    LogOutput("Teleported player to " + new Vector2(x,y));
                }
                else
                {
                    LogOutput("ERROR: No player currently exists in the scene.");
                }
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