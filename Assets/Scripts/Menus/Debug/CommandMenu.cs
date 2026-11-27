using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CommandMenu : MonoBehaviour
{
    private bool commandPromptOpen = false;
    [SerializeField] private GameObject CommandConsole;
    [SerializeField] private TMP_InputField commandPrompt;
    [SerializeField] private TextMeshProUGUI log;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private string[] previousCommands;
    private int previousCommandIndex;
    private bool darkmode = false;
    public string[] CommandList {get;} = {"help","save","loadscene <int>","sethealth <int>","setmaxhealth <int>","setcheckpoint <x> <y>","tp <x> <y>","reload","resetcheckpoint","return","darkMode <bool>","givecoin","takecoin","shake <float> <float>","burstshake <float> <float> <bool> <float>"}; 
    void Start()
    {
        Array.Resize(ref previousCommands,1);
        CommandConsole.SetActive(false);
        commandPromptOpen = false;
    }
    void OnLevelWasLoaded(int level)
    {
        if (darkmode)
        {
            if(!CurrentSceneIsALevel())
            {
                LogOutput("ERROR: The current scene, " + SceneManager.GetActiveScene().buildIndex + ", is not a level.");
                return;
            }
            Darkmode();
        }
    }
    void Update()
    {
        if (!commandPromptOpen && Input.GetKeyDown(KeyCode.Slash))
        {
            OpenCommandPrompt();
        }
        if (commandPromptOpen)
        {
            OpenUpdate();
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
    private void OpenUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseCommandPrompt();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            previousCommandIndex++;
            CyclePreviousCommands(true);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            previousCommandIndex--;
            CyclePreviousCommands(false);
        }
    }
    public void OpenCommandPrompt()
    {
        UserInput.Instance._playerInput.actions.Disable();
        commandPromptOpen = true;
        previousCommandIndex = 0;
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
        SaveCommandToList(commandInput);
        commandPrompt.Select();
        commandPrompt.ActivateInputField();
        string[] commandSplit = commandInput.ToLower().Split();
        Array.Resize(ref commandSplit,commandSplit.Length + 5);
        SceneTransition sceneTransition = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
        commandPrompt.text = "";
        int id = -1;
        int number = -1;
        bool state = true;
        float x = 0;
        float y = 0;
        float number1 = 0;
        float number2 = 0;
        float number3 = 0;
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
                if(!float.TryParse(commandSplit[1], out x) && commandSplit[1] != "")
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
                if(!float.TryParse(commandSplit[2], out y) && commandSplit[2] != "")
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
            case("reload"):
                if(sceneTransition == null)
                {
                    LogOutput("ERROR: No scene transition found.");
                    return;
                }
                else
                {
                    sceneTransition.TransitionToScene(SceneManager.GetActiveScene().buildIndex,1);
                    LogOutput("Reloading scene");
                }
                return;
            case("resetcheckpoint"):
                playerData.lastCheckpoint = new Vector2(0,0);
                LogOutput("Reset checkpoint");
                return;
            case("return"):
                PlayerStateManager.Instance.transform.position = playerData.lastCheckpoint;
                playerData.PlayerRb.position = playerData.lastCheckpoint;
                LogOutput("Reset checkpoint");
                return;
            case("darkmode"):
                if(bool.TryParse(commandSplit[1], out state))
                {
                    if(!CurrentSceneIsALevel())
                    {
                        LogOutput("ERROR: The current scene, " + SceneManager.GetActiveScene().buildIndex + ", is not a level.");
                        return;
                    }
                    darkmode = state;
                    Darkmode();
                }
                else
                {
                    if(!CurrentSceneIsALevel())
                    {
                        LogOutput("ERROR: The current scene, " + SceneManager.GetActiveScene().buildIndex + ", is not a level.");
                        return;
                    }
                    darkmode = !darkmode;
                    Darkmode();
                }
                return;
            case("givecoin"):
                playerData.hasHeartCoin = true;
                playerData.heartCoinSaved = true;
                if(HeartCoinIconHandler.Instance != null)
                {
                    HeartCoinIconHandler.Instance.UpdateGUI();
                }
                LogOutput("Gave Heart Coin to player.");
                return;
            case("takecoin"):
                playerData.hasHeartCoin = false;
                playerData.heartCoinSaved = false;
                if(HeartCoinIconHandler.Instance != null)
                {
                    HeartCoinIconHandler.Instance.UpdateGUI();
                }
                LogOutput("Took Heart Coin from player.");
                return;
            case("shake"):
                if(TriggerShake.Instance != null)
                {
                    if(!float.TryParse(commandSplit[1], out number1) && number1 < 0)
                    {
                        LogOutput("ERROR: No valid duration provided. Must be a positive number.");
                    }
                    if(!float.TryParse(commandSplit[2], out number2) && number < 0)
                    {
                        LogOutput("ERROR: No valid magnitude provided. Must be a positive number.");
                    }
                    TriggerShake.Instance.Shake(number1,number2);
                }
                else
                {
                    LogOutput("ERROR: No shakeable camera in this scene.");
                }
                return;
            case("burstshake"):
                if(TriggerShake.Instance != null)
                {
                    if(!float.TryParse(commandSplit[1], out number1) && number1 < 0)
                    {
                        LogOutput("ERROR: No valid magnitude provided. Must be a positive number.");
                    }
                    if(!float.TryParse(commandSplit[2], out number2) && number < 0)
                    {
                        LogOutput("ERROR: No valid lengthMult provided. Must be a positive number.");
                    }
                    if(!bool.TryParse(commandSplit[3], out state))
                    {
                        LogOutput("ERROR: No true/false provided for playSound.");
                    }
                    if(!float.TryParse(commandSplit[4], out number3) && number < 0)
                    {
                        LogOutput("ERROR: No valid overrideVolume provided. Must be a positive number. Set to 0 to not override.");
                    }
                    TriggerShake.Instance.BurstShake(number1,number2,state,number3);
                }
                else
                {
                    LogOutput("ERROR: No shakeable camera in this scene.");
                }
                return;
        }
        if(commandInput != "")
        {
            LogOutput("ERROR: Command \"" + commandSplit[0] + "\" is invalid or not found.");
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
    public void Darkmode()
    {
        GameObject globalLight2D = GameObject.Find("Global Light 2D");
        if(darkmode == true && CurrentSceneHasLightingAreas())
        {
            GameObject[] lightList = globalLight2D.GetComponent<LightingAreaHolder>().lightingObjects;
            for(int i = 0; i<lightList.Length; i++)
            {
                lightList[i].SetActive(false);
            }
            globalLight2D.GetComponent<LightingAreaHolder>().enabled = !darkmode;
        }
        else if (darkmode == false && CurrentSceneHasLightingAreas())
        {
            GameObject.Find("Loading Area 1").GetComponent<LightingAreaTrigger>().RefreshActiveLight();
        }
        globalLight2D.GetComponent<Light2D>().enabled = !darkmode;
        LogOutput("Set dark mode to " + darkmode);
    }
    public bool CurrentSceneIsALevel()
    {
        int[] levels = {2,3,4,13};
        for(int i = 0; i < levels.Length; i++)
        {
            if(SceneManager.GetActiveScene().buildIndex == levels[i])
            {
                return true;
            }
        }
        return false;
    }
    public bool CurrentSceneHasLightingAreas()
    {
        int[] levels = {2,4};
        for(int i = 0; i < levels.Length; i++)
        {
            if(SceneManager.GetActiveScene().buildIndex == levels[i])
            {
                return true;
            }
        }
        return false;
    }
    void SaveCommandToList(string newCommand)
    {
        Array.Resize(ref previousCommands, previousCommands.Length + 1);
        for(int i = previousCommands.Length-1; i > 1; i--)
        {
            previousCommands[i] = previousCommands[i-1];
        }
        previousCommands[1] = newCommand;
        previousCommands[0] = "";
    }
    void CyclePreviousCommands(bool direction)
    {
        if(previousCommandIndex < 0)
        {
            previousCommandIndex = 0;
        }
        if(previousCommandIndex >= previousCommands.Length)
        {
            previousCommandIndex = previousCommands.Length-1;
        }
        if(previousCommandIndex-1 == 0 && direction)
        {
            previousCommands[0] = commandPrompt.text;
        }
        if (previousCommands.Length != 1)
        {
            commandPrompt.text = previousCommands[previousCommandIndex];
        }
        commandPrompt.caretPosition = commandPrompt.text.Length;
    }
}