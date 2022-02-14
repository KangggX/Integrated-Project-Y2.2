using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ButtonType _buttonType;
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private GameObject _hologramPrompt;

    private GameManager _gameManager;
    private TargetManager _targetManager;
    private Target _target;
    //private TargetMovement _targetMovement;

    private void Awake()
    {
        _target = _targetObject.GetComponent<Target>();
        //_targetMovement = _targetObject.GetComponent<TargetMovement>();
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
            // Can only move target if it's not moving
            // Can only move target that is in use
            case ButtonType.CallbackTarget:
                if (!_targetManager.CheckTargetInUse())
                {
                    _target.InUse = true;
                }

                if (_target.InUse && !_target.IsMoving)
                {
                    _target.MoveTarget();
                    _hologramPrompt.SetActive(false);
                }

                break;

            // Clear all hole instances in target's child
            // Reset points on display
            // Submit points to database
            // Move target back to inner position if target is currently at outer position
            // Reset weapons to default position and ammo
            case ButtonType.ClearLane:
                if (_target.InUse && !_target.IsMoving)
                {
                    _hologramPrompt.SetActive(true);
                }

                break;
        }
    }
}

public enum ButtonType { CallbackTarget, ClearLane };