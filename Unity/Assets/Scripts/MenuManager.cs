using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;

/**
Author: Kang

Name of Class: MenuManager

Description of Class: This class manages the Main Menu

Date Created: 18/02/2022
**/
public class MenuManager : MonoBehaviour
{
    private AuthManager _authManager;

    public TMP_Text displayName;

    //Check for transitions
    public static bool ppTransition = false;
    public static bool leaderboardTransition = false;
    public static bool playTransition = false;

    private void Start()
    {
        _authManager = FindObjectOfType<AuthManager>();

        displayName.text = "Welcome " + _authManager.GetCurrentUserDisplayName();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Hub");
    }

    public void BackToAuth()
    {
        SceneManager.LoadScene("Auth");
    }

    public void QuitGame()
    {
        AudioClipManager.PlaySound("button");
        Application.Quit();
    }
}
