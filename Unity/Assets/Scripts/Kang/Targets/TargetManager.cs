using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kang Xuan

Name of Class: TargetManager

Description of Class: Manages all the Indoor Shooting targets

Date Created: 18/02/2022
**/
public class TargetManager : MonoBehaviour
{
    private Target[] _targetList;
    public Target targetInUse;

    private void Start()
    {
        _targetList = FindObjectsOfType<Target>();
    }

    // Check if there are any target in use
    public bool CheckTargetInUse()
    {
        foreach (Target target in _targetList)
        {
            if (target.InUse)
            {
                targetInUse = target;
                return true;
            }
        }

        return false;
    }

    // Reset used target, then remove _targetInUse
    public void ResetTarget()
    {
        targetInUse.CheckIfTargetIsOut();
        targetInUse.ClearBulletHoleInstances();
        targetInUse.ResetPoints();

        targetInUse.InUse = false;
        targetInUse = null;
    }
}
