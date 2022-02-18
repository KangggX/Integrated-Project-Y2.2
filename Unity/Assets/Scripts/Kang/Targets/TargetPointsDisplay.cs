using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
Author: Kang Xuan

Name of Class: TargetPointsDisplay

Description of Class: Update the points in the current lane

Date Created: 18/02/2022
**/
public class TargetPointsDisplay : MonoBehaviour
{
    [SerializeField] private Target _target;
    [SerializeField] private TextMeshProUGUI _pointsText;

    private void OnEnable()
    {
        _target.OnPointsChanged += UpdateText;
    }

    private void OnDisable()
    {
        _target.OnPointsChanged -= UpdateText;
    }

    // Function that subscribes to the target
    private void UpdateText(int points)
    {
        _pointsText.text = points.ToString();
    }
}
