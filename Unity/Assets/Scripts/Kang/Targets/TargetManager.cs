using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static event Action<int> OnPointsChanged;

    [SerializeField] private Transform[] _targetParts;
    private int _totalPoints;

    public int TotalPoints
    {
        get { return _totalPoints; }
        set { _totalPoints = value; OnPointsChanged?.Invoke(_totalPoints); }
    }

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

    public void ResetPoints()
    {
        TotalPoints = 0;
    }
}
