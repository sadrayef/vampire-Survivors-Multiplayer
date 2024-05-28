using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        GamePlay,
        Paused,
        GameOver
    }

    public GameState currentState;
    public GameState previousState;

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    [Header("Current Stat Displays")]
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
    public List<UnityEngine.UI.Image> chosenWeaponsUI = new List<UnityEngine.UI.Image>(6);
    public List<UnityEngine.UI.Image> chosenPassiveUI = new List<UnityEngine.UI.Image>(6);


    public bool isGameOver = false;

    void Awake()
    {
        if(instance == null)
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


    void Update()
    {
        switch(currentState)
        {
            case GameState.GamePlay:
                CheckForPauseAndResume();
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

            default:
                Debug.Log("State Does Not Exist");
                break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

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

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }
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

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }
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
}
