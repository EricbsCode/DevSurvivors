// GameController.cs
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    // Singleton pattern
    public static GameController instance;

    public GameObject levelUpPanel;
    public int playerExperience = 0;
    public int playerLevel = 0;
    public GameObject player; // Add this line to reference the player GameObject
    public LevelUpOptions[] levelUpOptionsArray; // Add this line to store options
    public Text[] LevelUpDisplayDescription = new Text[4];
    public Image[] optionImages = new Image[4];
    public Text[] optionLevelText = new Text[4];
    public Button[] LevelUpButtons = new Button[4];
    private bool LevelUpComplete = false;
    private ItemStackOverflow stackOverflowItem;
    public ItemExpVacuum ExpVacuum;



    private void Awake()
    {
        // Ensure there is only one instance of GameController
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy this new instance
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        stackOverflowItem = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemStackOverflow>();
        ExpVacuum = GameObject.FindGameObjectWithTag("ExpPickUpCollider").GetComponent<ItemExpVacuum>();
        if (stackOverflowItem == null)
        {
            Debug.LogError("ItemStackOverflow script not found on the player GameObject.");
        }
        player = GameObject.FindGameObjectWithTag("Player");
        levelUpPanel.SetActive(false);
        //StartCoroutine(GrantXpCoroutine(500));
    }


    private void DisplayLevelUpOptions()
    {
        ShuffleOptionsArray();
        // This method can be called to set up your UI with the options
        // For simplicity, let's assume you have four buttons for the choices

        for (int i = 0; i < 4; i++)
        {
            // Example: Set the text of each UI Text element to an upgrade stat text from the options
            SetLevelUpDisplayDescription(i, levelUpOptionsArray[i].UpgradeStatTexts[levelUpOptionsArray[i].ChosenCount]);
            SetLevelUpDisplayImage(i, levelUpOptionsArray[i].Picture);
            SetLevelUpDisplayLevel(i, levelUpOptionsArray[i].ItemLevel);
            SetButtonFunctionality(i);
        }
    }

    private void SetLevelUpDisplayDescription(int optionIndex, string text)
    {
        // Replace this with your actual method to set the text of the UI Text elements
        // For example, if you have Text components on your buttons, you can do something like:
        LevelUpDisplayDescription[optionIndex].text = text;
    }

    private void SetLevelUpDisplayImage(int optionIndex, Sprite sprite)
    {
        // Replace this with your actual method to set the image of the UI Image elements
        // For example, if you have Image components on your buttons, you can do something like:
        optionImages[optionIndex].sprite = sprite;
    }

        private void SetLevelUpDisplayLevel(int optionIndex, int level)
    {
        // Replace this with your actual method to set the text of the UI Text elements for current level
        // For example, if you have Text components on your buttons, you can do something like:
        optionLevelText[optionIndex].text = "Level " + level.ToString();
    }

    private void SetButtonFunctionality(int optionIndex)
    {
        // Get the button component for the specified index
        Button button = LevelUpButtons[optionIndex]; // Replace with your actual button array

        // Remove existing listeners to avoid duplicates
        button.onClick.RemoveAllListeners();

        // Add a listener to the button's click event
        button.onClick.AddListener(() => HandleChoice(optionIndex));
    }


    private void ShuffleOptionsArray()
    {
        int n = levelUpOptionsArray.Length;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            LevelUpOptions temp = levelUpOptionsArray[k];
            levelUpOptionsArray[k] = levelUpOptionsArray[n];
            levelUpOptionsArray[n] = temp;
        }
    }


    public void GrantXp(int xp)
    {
        StartCoroutine(GrantXpCoroutine(xp));
    }

    private System.Collections.IEnumerator GrantXpCoroutine(int xp)
    {
        // Implement your logic for granting experience points here
        // For example, you might increase a player's experience points.
        // Make sure to replace this with your actual implementation.
        playerExperience += xp;
        if (playerExperience >= XpPerLevel(playerLevel))
        {
            playerLevel++;
            LevelUpComplete = false; // Reset the flag
            LevelUp();

            // Wait until LevelUpComplete is set to true
            while (!LevelUpComplete)
            {
                yield return null; // Wait for the next frame
            }

            // Grant XP recursively for the next level
            yield return StartCoroutine(GrantXpCoroutine(-XpPerLevel(playerLevel - 1)));
        }
    }

    public int XpPerLevel(int playerLevel)
    {
        if(playerLevel == 0)
        {
            return 5;
        }
        else
        return (int)Math.Pow(playerLevel, 1.5)-(XpPerLevel(playerLevel - 1)) + 10;
    }

    private void LevelUp()
    {
        DisplayLevelUpOptions();
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void HandleChoice(int optionIndex)
    {
        // Implement logic for the chosen option
        // You can access the corresponding LevelUpOptions using optionIndex
        LevelUpOptions chosenOption = levelUpOptionsArray[optionIndex];
        chosenOption.ItemLevel++;
        chosenOption.ChosenCount++;
        CallSelectedFunction(chosenOption);

        // Continue the game
        ResumeGame();
    }

    private void CallSelectedFunction(LevelUpOptions chosenOption)
{
    // Using a switch statement to determine the function to invoke
    switch (chosenOption.FunctionID)
    {
        case 1:
            Debug.Log("Upgrading Cursor");
            break;
        case 2:
            Debug.Log("Upgrading Keyboard");
            break;
        case 3:
            Debug.Log("Upgrading Bug Spray");
            break;
        case 4:
            Debug.Log("Upgrading Minecraft Sword");
            break;
        case 5:
            stackOverflowItem.LevelUpStackOverflow();
            break;
        case 6:
            Debug.Log("Upgrading Energy Drink");
            break;
        case 7:
            Debug.Log("Upgrading Debugger");
            break;
        case 8:
            ExpVacuum.LevelExpVacuum(chosenOption.ChosenCount);
            break;
        case 9:
            // Insert respective function for case 29 here
            break;
        case 10:
            // Insert respective function for case 30 here
            break;
        case 11:
            // Insert respective function for case 31 here
            break;
        case 12:
            // Insert respective function for case 32 here
            break;
        case 13:
            // Insert respective function for case 1 here
            break;
        case 14:
            // Insert respective function for case 2 here
            break;
        case 15:
            // Insert respective function for case 3 here
            break;
        case 16:
            // Insert respective function for case 4 here
            break;
        case 17:
            // Insert respective function for case 1 here
            break;
        case 18:
            // Insert respective function for case 2 here
            break;
        case 19:
            // Insert respective function for case 3 here
            break;
        case 20:
            // Insert respective function for case 4 here
            break;
        case 21:
            // Insert respective function for case 29 here
            break;
        case 22:
            // Insert respective function for case 30 here
            break;
        case 23:
            // Insert respective function for case 31 here
            break;
        case 24:
            // Insert respective function for case 32 here
            break;
        case 25:
            // Insert respective function for case 29 here
            break;
        case 26:
            // Insert respective function for case 30 here
            break;
        case 27:
            // Insert respective function for case 31 here
            break;
        case 28:
            // Insert respective function for case 32 here
            break;
        case 29:
            // Insert respective function for case 29 here
            break;
        case 30:
            // Insert respective function for case 30 here
            break;
        case 31:
            // Insert respective function for case 31 here
            break;
        case 32:
            // Insert respective function for case 32 here
            break;

        default:
            Debug.LogError($"Function ID {chosenOption.FunctionID} not found.");
            break;
    }
}


    private void ResumeGame()
    {
        levelUpPanel.SetActive(false);

        Time.timeScale = 1f;

        LevelUpComplete = true;
    }
    // Other GameController code...
}
