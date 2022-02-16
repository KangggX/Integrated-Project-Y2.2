using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Script to display profile text in the Profile Menu
/// </summary>
public class MenuProfile : MonoBehaviour
{
    [Header("Skiing Text")]
    [SerializeField] private TextMeshProUGUI _skiingFastestTimeText;
    [SerializeField] private TextMeshProUGUI _skiingTotalTimeText;
    [SerializeField] private TextMeshProUGUI _skiingTotalGameText;

    [Header("Indoor Shooting Text")]
    [SerializeField] private TextMeshProUGUI _indoorHighestPointsText;
    [SerializeField] private TextMeshProUGUI _indoorTotalPointsText;
    [SerializeField] private TextMeshProUGUI _indoorTotalGameText;

    [Header("Outdoor Shooting Text")]
    [SerializeField] private TextMeshProUGUI _outdoorHighestPointsText;
    [SerializeField] private TextMeshProUGUI _outdoorTotalPointsText;
    [SerializeField] private TextMeshProUGUI _outdoorTotalGameText;

    [Header("Managers")]
    private string _userID;
    private AuthManager _authManager;
    private SimpleFirebaseManager _firebaseManager;

    private void Start()
    {
        _authManager = FindObjectOfType<AuthManager>();
        _firebaseManager = FindObjectOfType<SimpleFirebaseManager>();

        _userID = _authManager.auth.CurrentUser.UserId;

        UpdateProfileInfo();
    }

    // Update profile info to the UI
    private async void UpdateProfileInfo()
    {
        SimplePlayerStats stats = await _firebaseManager.GetPlayerStats(_userID);
        Debug.Log(stats.fastestTime);

        // Updating stats for Skii UI
        _skiingFastestTimeText.text = stats.fastestTime.ToString();
        _skiingTotalTimeText.text = stats.totalTime.ToString();
        _skiingTotalGameText.text = stats.totalGame.ToString();

        // Updating stats for Indoor Shooting UI
        _indoorHighestPointsText.text = stats.indoorPoints.ToString();
        _indoorTotalPointsText.text = stats.indoorTotalPoints.ToString();
        _indoorTotalGameText.text = stats.indoorTotalGame.ToString();

        // Updating stats for Outdoor Shooting UI
        _outdoorHighestPointsText.text = stats.outdoorPoints.ToString();
        _outdoorTotalPointsText.text = stats.outdoorTotalPoints.ToString();
        _outdoorTotalGameText.text = stats.outdoorTotalGame.ToString();
    }
}
