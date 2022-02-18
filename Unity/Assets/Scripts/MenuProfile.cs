using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
Author: Kirdesh

Name of Class: MenuProfile

Description of Class: Handles the Profile page in the Main Menu

Date Created: 18/02/2022
**/
public class MenuProfile : MonoBehaviour
{
    [Header("Skiing Text")]
    [SerializeField] private TextMeshProUGUI skiingFastestTimeText;
    [SerializeField] private TextMeshProUGUI skiingTotalTimeText;
    [SerializeField] private TextMeshProUGUI skiingTotalGameText;

    [Header("Indoor Shooting Text")]
    [SerializeField] private TextMeshProUGUI indoorHighestPointsText;
    [SerializeField] private TextMeshProUGUI indoorTotalPointsText;
    [SerializeField] private TextMeshProUGUI indoorTotalGameText;

    [Header("Outdoor Shooting Text")]
    [SerializeField] private TextMeshProUGUI outdoorHighestPointsText;
    [SerializeField] private TextMeshProUGUI outdoorTotalPointsText;
    [SerializeField] private TextMeshProUGUI outdoorTotalGameText;

    [Header("Managers")]
    private string userID;
    private AuthManager authManager;
    private SimpleFirebaseManager firebaseManager;

    private void Start()
    {
        authManager = FindObjectOfType<AuthManager>();
        firebaseManager = FindObjectOfType<SimpleFirebaseManager>();

        userID = authManager.auth.CurrentUser.UserId;

        UpdateProfileInfo();
    }

    // Update profile info to the UI
    private async void UpdateProfileInfo()
    {
        SimplePlayerStats stats = await firebaseManager.GetPlayerStats(userID);
        Debug.Log(stats.fastestTime);

        // Updating stats for Skii UI
        skiingFastestTimeText.text = stats.fastestTime.ToString();
        skiingTotalTimeText.text = stats.totalTime.ToString();
        skiingTotalGameText.text = stats.totalGame.ToString();

        // Updating stats for Indoor Shooting UI
        indoorHighestPointsText.text = stats.indoorPoints.ToString();
        indoorTotalPointsText.text = stats.indoorTotalPoints.ToString();
        indoorTotalGameText.text = stats.indoorTotalGame.ToString();

        // Updating stats for Outdoor Shooting UI
        outdoorHighestPointsText.text = stats.outdoorPoints.ToString();
        outdoorTotalPointsText.text = stats.outdoorTotalPoints.ToString();
        outdoorTotalGameText.text = stats.outdoorTotalGame.ToString();
    }
}
