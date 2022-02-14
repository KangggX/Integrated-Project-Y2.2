using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Weapon[] _weaponList;
    private TargetManager _targetManager;
    private AuthManager _authManager;
    private SimpleFirebaseManager _firebaseManager;

    // for outdoor scene stuff - Dan
    private SpawnRandom SpawnRandom;
    public int OutdoorPoints = 0;
    public TMP_Text OutdoorPointsText;
    //-----------------------------

    private void Awake()
    {
        _authManager = GetComponent<AuthManager>();
        _firebaseManager = GetComponent<SimpleFirebaseManager>();
    }

    private void Start()
    {
        _weaponList = FindObjectsOfType<Weapon>();
        _targetManager = FindObjectOfType<TargetManager>();
    }

    private void Update()
    {
        // for outdoor stuff - Dan
        if (OutdoorPointsText != null)
        {
            OutdoorPointsText.text = string.Format("Points : {0}",OutdoorPoints);
        }
        //-----------------------------
    }

    //for outdoor stuff - Dan
    public void stopOutdoor()
    {
        SpawnRandom.isGameActive = false;
    }
    //---------------------------

    public void ResetIndoorShooter()
    {
        UpdatePlayerIndoorStats();

        foreach (Weapon weapon in _weaponList)
        {
            weapon.ResetAmmo();
        }

        _targetManager.ResetTarget();
    }

    // Send data to FirebaseManager to update Player Stats (Indoor)
    private void UpdatePlayerIndoorStats()
    {
        string uuid = _authManager.auth.CurrentUser.UserId;
        string displayName = _authManager.auth.CurrentUser.DisplayName;
        int points = _targetManager.targetInUse.TotalPoints;

        _firebaseManager.UpdatePlayerIndoorStats(uuid, displayName, points);
    }
}
