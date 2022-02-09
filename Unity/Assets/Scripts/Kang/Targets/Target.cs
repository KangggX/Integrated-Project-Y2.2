using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<int> OnPointsChanged;

    private TargetManager _targetManager;

    [SerializeField] private Transform[] _targetParts;
    private BoxCollider[] _targetPartsColliders;

    private int _totalPoints;
    private bool _inUse;

    private void Awake()
    {
        _targetPartsColliders = GetComponentsInChildren<BoxCollider>();
    }

    private void Start()
    {
        _targetManager = FindObjectOfType<TargetManager>();   

        EnableColliders();
    }

    public bool InUse
    {
        get { return _inUse; }
        set { _inUse = value; _targetManager.CheckTargetInUse(); EnableColliders(); }
    }

    public int TotalPoints
    {
        get { return _totalPoints; }
        set { _totalPoints = value; OnPointsChanged?.Invoke(_totalPoints); }
    }

    private void EnableColliders()
    {
        foreach (var collider in _targetPartsColliders)
        {
            collider.enabled = !collider.enabled;
        }
    }

    // Cleaer bullet holes in the child of the target
    public void ClearBulletHoleInstances()
    {
        foreach (Transform parts in _targetParts)
        {
            if (parts.childCount > 0)
            {
                foreach (Transform holeInstance in parts)
                {
                    Destroy(holeInstance.gameObject);
                }
            }
        }
    }

    // Reset points
    public void ResetPoints()
    {
        TotalPoints = 0;
    }
}
