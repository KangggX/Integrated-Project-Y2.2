using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to handle the Main Menu itself (only)
/// </summary>
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

    public void Tutorial()
    {
        playTransition = true;
        AudioClipManager.PlaySound("button");
        SceneManager.LoadScene("SampleScene");
    }
    public void SpeedRun()
    {
        playTransition = true;
        AudioClipManager.PlaySound("button");
        SceneManager.LoadScene("SpeedRun");
    }
    public void QuitGame()
    {
        AudioClipManager.PlaySound("button");
        Application.Quit();
    }

    public void PlayerStats()
    {
        ppTransition = true;
        //AudioClipManager.PlaySound("button");
        SceneManager.LoadScene(3);
    }

    public void Leaderboard()
    {
        leaderboardTransition = true;
        AudioClipManager.PlaySound("button");
        SceneManager.LoadScene(2);
    }
}
