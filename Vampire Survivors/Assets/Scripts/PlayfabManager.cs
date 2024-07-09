using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System;

public class PlayfabManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowParent;

    public Text messageText;
    public GameObject loginPage;
    public InputField passwordInput;
    public InputField emailInput;
    public Text AccountDetails;


    public void RegisterButton()
    {
        if(passwordInput.text.Length < 6)
        {
            messageText.text = "Password too short!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess,  OnError);
        
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
        AccountDetails.text = emailInput.text;
        loginPage.SetActive(false);
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in!";
        AccountDetails.text = emailInput.text;
        loginPage.SetActive(false);
    }

    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "A5AF8"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
        
    }

    private void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageText.text = "Password reset mail sent!";
    }


    private void Start()
    {
        Login();   //it was before login with email address
        //loginPage.SetActive(true);
        //AccountDetails.text = "Playing as a Guest.";
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Successful login/account created!");
    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int killingScore)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate
                {
                    StatisticName = "PlatformScore",
                    Value = killingScore
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successful leaderboard sent!");
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "PlatformScore",
            StartPosition = 1,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();

            texts[0].text = item.Position.ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();  

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
}

