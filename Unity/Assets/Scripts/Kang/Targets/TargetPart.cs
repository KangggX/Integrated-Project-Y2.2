using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPart : MonoBehaviour
{
    [SerializeField] private int _pointValue;
    private Target _target;

    private void Start()
    {
        _target = GetComponentInParent<Target>();
    }

    public void Hit()
    {
        _target.TotalPoints += _pointValue;
    }
}
