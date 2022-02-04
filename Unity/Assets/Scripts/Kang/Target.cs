using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public static event Action<int> OnPointsChanged;

    [SerializeField] private int _pointValue;
    private TargetManager _targetManager;

    private void Start()
    {
        _targetManager = GetComponentInParent<TargetManager>();
    }

    public void Hit()
    {
        _targetManager.TotalPoints += _pointValue;
        OnPointsChanged?.Invoke(_targetManager.TotalPoints);
    }
}
