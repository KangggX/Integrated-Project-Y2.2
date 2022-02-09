using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Weapon[] _weaponList;
    private TargetManager _targetManager;
    private AuthManager _authManager;
    private SimpleFirebaseManager _firebaseManager;

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

    public void ResetCurrentGame()
    {
        UpdatePlayerStats();

        foreach (Weapon weapon in _weaponList)
        {
            weapon.ResetAmmo();
        }

        //_targetManager.ClearBulletHoleInstances();
        //_targetManager.ResetPoints();
        _targetManager.ResetTarget();
    }

    // Send data to FirebaseManager to update Player Stats
    private void UpdatePlayerStats()
    {
        string uuid = _authManager.auth.CurrentUser.UserId;
        string displayName = _authManager.auth.CurrentUser.DisplayName;
        int points = _targetManager.targetInUse.TotalPoints;

        _firebaseManager.UpdatePlayerIndoorStats(uuid, displayName, points);
    }
}
