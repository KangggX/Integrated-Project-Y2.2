using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPart : MonoBehaviour
{
    [SerializeField] private int _pointValue;
    private Target _targetManager;

    private void Start()
    {
        _targetManager = GetComponentInParent<Target>();
    }

    public void Hit()
    {
        _targetManager.TotalPoints += _pointValue;
    }
}
