using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static event Action<int> OnPointsChanged;

    private Target[] _targetList;
    public Target targetInUse;

    private void Start()
    {
        _targetList = FindObjectsOfType<Target>();
    }

    // Check if there are any target in use
    public bool AnyTargetInUse()
    {
        foreach (Target target in _targetList)
        {
            if (target.targetInUse)
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
        targetInUse.ClearBulletHoleInstances();
        targetInUse.ResetPoints();

        targetInUse = null;
    }
}
