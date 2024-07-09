using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GameManager;
//the most important in the whole game
//---------------------------------------------------------------------


public class GameManager : MonoBehaviour
{
    //---------------------------------------------------------------------

    public static GameManager instance;
    public enum GameState // 4 game states
    {
        GamePlay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;
    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject levelUpScreen;

    [Header("Current Stat Displays")]
    public Text healthDisplay;
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Display")]
    public UnityEngine.UI.Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    public Text timeSurvivedDisplay;
    public Text killedEnemyDisplay;
    //public Text highestDisplay;
    public Text collectedCoinsDisplay;
    public List<UnityEngine.UI.Image> chosenWeaponsUI = new List<UnityEngine.UI.Image>(6);
    public List<UnityEngine.UI.Image> chosenPassiveUI = new List<UnityEngine.UI.Image>(6);


    [Header("Stop Watch")] // for yhe timer
    public float timeLimit; // in seconds
    float stopWatchTime ;
    public Text stopWatchDisplay;

    public bool isGameOver = false;

    public bool choosingUpgrade;

    [Header("Killed Enemy")] // for the enemy counter
    public Text killedEnemyText;
    //public Text highestKilledEnemyText;
    float killedEnemyCount;
    float highestScore;
    
    [Header("Coins")]
    public Text coinsText;
    public Text allCoinsText;
    int allCoins;
    int coinsCount;


    //---------------------------------------------------------------------

    public GameObject heroObject;

    //---------------------------------------------------------------------

    void Awake()
    {
        //PlayerPrefs.SetFloat("Killed Enemies", 0);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Extra" + this + "deleted");
            Destroy(gameObject);
        }
        DisableScreens();
    }

    //---------------------------------------------------------------------


    void Update()
    {
        switch(currentState)
        {
            case GameState.GamePlay:
                CheckForPauseAndResume();
                UpdateStopWatch(); // The stopwathc will run only in gameplay game state.
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if(!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    Debug.Log("Game Over Madar sag");
                    DisplayResults();
                }
                break;
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    Debug.Log("Upgrades shown");
                    levelUpScreen.SetActive(true);
                }
                break;

            default:
                Debug.Log("State Does Not Exist");
                break;
        }
    }

    //---------------------------------------------------------------------

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    //---------------------------------------------------------------------

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; //stop the game
            pauseScreen.SetActive(true);         
            Debug.Log("PAUSED");
        }
    }

    //---------------------------------------------------------------------

    public void ResumeGame() 
    {
        if ((currentState == GameState.Paused))
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("RESUMED");
        }

    }

    //---------------------------------------------------------------------

    public void GameOver()
    {
        
        if (killedEnemyCount > (int)PlayerPrefs.GetFloat("Killed Enemies"))
        {
            highestScore = killedEnemyCount;
            PlayerPrefs.SetFloat("Killed Enemies", highestScore);

        }
        //highestKilledEnemyText.text = highestScore.ToString();
        

        PlayerPrefs.Save();
        allCoins = (int)PlayerPrefs.GetFloat("All coins");
        allCoins += coinsCount;
        allCoinsText.text = allCoins.ToString();
        PlayerPrefs.SetFloat("All coins", allCoins);
        PlayerPrefs.Save();
        /*
        allCoinsText.text = allCoins.ToString();
        PlayerPrefs.SetFloat("All coins", allCoins);
        PlayerPrefs.Save();
        */
   

    //highestDisplay.text = highestKilledEnemyText.text;
    killedEnemyDisplay.text = killedEnemyText.text;
        timeSurvivedDisplay.text = stopWatchDisplay.text;
        collectedCoinsDisplay.text = coinsText.text;
        ChangeState(GameState.GameOver);

       
        //highestKilledEnemyText.text = highestScore.ToString();
        PlayfabManager pm = GameObject.FindWithTag("PlayfabManager").GetComponent<PlayfabManager>();
        pm.SendLeaderboard(10);
    }

    //---------------------------------------------------------------------

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    //---------------------------------------------------------------------

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyUp(KeyCode.E))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

    }

    //---------------------------------------------------------------------

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    //---------------------------------------------------------------------

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    //---------------------------------------------------------------------

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    //---------------------------------------------------------------------

    public void AssighnChosenPassiveAndWeaponsUI(List<UnityEngine.UI.Image> chosenWeaponData, List<UnityEngine.UI.Image> chosenPassiveData)
    {
        if(chosenWeaponData.Count != chosenWeaponsUI.Count || chosenPassiveData.Count != chosenPassiveUI.Count)
        {
            Debug.Log("chosen passive items and weapons data lists have different lenghts");
            return;
        }
        for ( int i = 0; i < chosenWeaponsUI.Count; i++ )
        {
            if (chosenWeaponData[i].sprite)
            {
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponData[i].sprite;
            }
            else
            {
                chosenWeaponsUI[i].enabled = false;
            }
        }

        //passive items
        for (int i = 0; i < chosenPassiveUI.Count; i++)
        {
            if (chosenPassiveData[i].sprite)
            {
                chosenPassiveUI[i].enabled = true;
                chosenPassiveUI[i].sprite = chosenPassiveData[i].sprite;
            }
            else
            {
                chosenPassiveUI[i].enabled = false;
            }
        }
    }

    //---------------------------------------------------------------------


    void UpdateStopWatch()
    {
        stopWatchTime += Time.deltaTime;

        UpdateStopWatchDisplay();

        if(stopWatchTime >= timeLimit) // ##########
        {
            GameOver();
        }
    }

    //---------------------------------------------------------------------

    void UpdateStopWatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopWatchTime / 60);
        int seconds = Mathf.FloorToInt(stopWatchTime % 60);

        stopWatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //---------------------------------------------------------------------

    public void UpdateKilledenemyCount()
    {
        killedEnemyCount++;
        /*
        if (killedEnemyCount >= PlayerPrefs.GetFloat("Killed Enemies"))
        {
            highestScore = killedEnemyCount;

        }
        */
        //highestKilledEnemyText.text = highestScore.ToString();
        //PlayerPrefs.SetFloat("Killed Enemies", highestScore);
        //PlayerPrefs.Save();
        
    }

    //---------------------------------------------------------------------

    public void UpdateKilledEnemyCountDisplay()
    {
        killedEnemyText.text = killedEnemyCount.ToString();
    }

    //---------------------------------------------------------------------
     public void UpdateCoinsCount()
    {
        coinsCount++;
        
        //allCoins += coinsCount;
        //allCoins += 1;
        /*
        allCoinsText.text = allCoins.ToString();
        PlayerPrefs.SetFloat("All coins", allCoins);
        PlayerPrefs.Save();
        */
    }

    public void UpdateCoinsCountBy50()
    {
        coinsCount+= 50;

        //allCoins += coinsCount;
        //allCoins += 1;
        /*
        allCoinsText.text = allCoins.ToString();
        PlayerPrefs.SetFloat("All coins", allCoins);
        PlayerPrefs.Save();
        */
    }

    //---------------------------------------------------------------------

    public void UpdateCoinsCountDisplay()
    {
        coinsText.text = coinsCount.ToString();
    }

    //---------------------------------------------------------------------

    public void startLevelUp()
    {
        ChangeState(GameState.LevelUp);
        heroObject.SendMessage("RemoveAndApplyUpgrades"); 
    }

    //---------------------------------------------------------------------

    public void endLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.GamePlay);
    }

    //---------------------------------------------------------------------

}
