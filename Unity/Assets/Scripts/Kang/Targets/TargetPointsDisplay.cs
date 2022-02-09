using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void UpdateText(int points)
    {
        _pointsText.text = points.ToString();
    }
}
