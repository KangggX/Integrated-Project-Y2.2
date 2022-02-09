using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ButtonType _buttonType;
    [SerializeField] private GameObject _targetObject;

    private GameManager _gameManager;
    private TargetManager _targetManager;
    private Target target;
    private TargetMovement targetMovement;

    private void Awake()
    {
        target = _targetObject.GetComponent<Target>();
        targetMovement = _targetObject.GetComponent<TargetMovement>();
    }

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _targetManager = FindObjectOfType<TargetManager>();
    }

    public void TriggerInteraction()
    {
        switch( _buttonType )
        {
            // If no lane in use, current lane will be set to used and then move target to outer position
            // If target is at outer positon, move it back to inner position
            case ButtonType.CallbackTarget:
                targetMovement.MoveTarget();

                if (_targetManager.targetInUse == null)
                {
                    target.InUse = true;
                }

                break;

            // Clear all hole instances in target's child
            // Reset points on display
            // Submit points to database
            // Move target back to inner position if target is currently at outer position
            // Reset weapons to default position and ammo
            case ButtonType.ClearLane:
                _gameManager.ResetCurrentGame();

                break;
        }
    }
}

public enum ButtonType { CallbackTarget, ClearLane };