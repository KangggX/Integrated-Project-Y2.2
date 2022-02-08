using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButton : MonoBehaviour, IInteractable
{
    [SerializeField] private ButtonType _buttonType;
    [SerializeField] private GameObject _targetObject;

    private UIManager _uiManager;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void TriggerInteraction()
    {
        switch( _buttonType )
        {
            case ButtonType.CallbackTarget:
                TargetMovement targetMovement = _targetObject.GetComponent<TargetMovement>();

                if ( !targetMovement.CanMove )
                {
                    targetMovement.CanMove = true;
                }

                break;

            case ButtonType.ClearHoleInstance:
                _uiManager.PromptClearTarget(true);

                break;
        }
    }
}

public enum ButtonType { CallbackTarget, ClearHoleInstance };