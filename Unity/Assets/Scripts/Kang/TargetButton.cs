using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetButton : MonoBehaviour, IInteractable
{
    public static event Action<bool> OnClearClick;

    [SerializeField] private ButtonType _buttonType;
    [SerializeField] private GameObject _targetObject;

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
                OnClearClick?.Invoke( true );

                break;
        }
    }
}

public enum ButtonType { CallbackTarget, ClearHoleInstance };