using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kang Xuan

Name of Class: TargetPart

Description of Class: Script that is assigned to the Head and Body of the Indoor Shooting targets

Date Created: 18/02/2022
**/
public class TargetPart : MonoBehaviour
{
    [SerializeField] private int _pointValue;
    private Target _target;

    private void Start()
    {
        _target = GetComponentInParent<Target>();
    }

    // When called, increment point of the parent Target
    public void Hit()
    {
        _target.TotalPoints += _pointValue;
    }
}
