using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HologramResults : MonoBehaviour
{
    private AuthManager _authManager;
    private SimpleFirebaseManager _firebaseManager;
    private string _userID;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _highestPointsField;
    [SerializeField] private TextMeshProUGUI _totalPointsField;
    [SerializeField] private TextMeshProUGUI _totalGameField;

    private void OnEnable()
    {
        UpdateResultUI();
    }

    private void Start()
    {
        _authManager = FindObjectOfType<AuthManager>();
        _firebaseManager = FindObjectOfType<SimpleFirebaseManager>();

        _userID = _authManager.auth.CurrentUser.UserId;

        UpdateResultUI();
        gameObject.SetActive(false);
    }

    private async void UpdateResultUI()
    {
        SimplePlayerStats playerStats;
        playerStats = await _firebaseManager.GetPlayerStats(_userID);

        if (playerStats != null)
        {
            _highestPointsField.text = playerStats.indoorPoints.ToString();
            _totalPointsField.text = playerStats.indoorTotalPoints.ToString();
            _totalGameField.text = playerStats.indoorTotalGame.ToString();
        }
    }

    // Coroutine to turn off the results after 8s
    private IEnumerator AwakeTimeout()
    {
        yield return new WaitForSeconds(8);
        gameObject.SetActive(false);
    }
}
