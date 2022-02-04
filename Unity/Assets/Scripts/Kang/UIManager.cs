using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private GameObject _magazineEmptyPrompt;

    [SerializeField] private GameObject _clearTargetPromptGO;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _pointsText;

    private void OnEnable()
    {
        Target.OnPointsChanged += UpdatePointsText;
        TargetButton.OnClearClick += PromptClearTarget;
    }

    private void OnDisable()
    {
        Target.OnPointsChanged -= UpdatePointsText;
        TargetButton.OnClearClick -= PromptClearTarget;
    }

    public void UpdatePointsText(int points)
    {
        _pointsText.text = "Current Points: " + points.ToString();
    }

    public void PromptClearTarget(bool state)
    {
        _clearTargetPromptGO.SetActive(state);
    }

    public static void DisplayMagazineError()
    {
        Debug.Log("Ran out of ammo!");
    }
}
