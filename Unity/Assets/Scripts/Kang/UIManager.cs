using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _magazineEmptyPrompt;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _pointsText;

    private void OnEnable()
    {
        Target.OnPointsChanged += DisplayPointsText;
    }

    private void OnDisable()
    {
        Target.OnPointsChanged -= DisplayPointsText;
    }

    public void DisplayPointsText(int points)
    {
        _pointsText.text = "Current Points: " + points.ToString();
    }

    public static void DisplayMagazineError()
    {
        Debug.Log("Ran out of ammo!");
    }
}
