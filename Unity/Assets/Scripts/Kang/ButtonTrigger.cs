using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour, IInteractable
{
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
                TargetManager targetManager = _targetObject.GetComponent<TargetManager>();

                targetManager.ClearBulletHoleInstances();

                break;
        }
    }
}

public enum ButtonType { CallbackTarget, ClearHoleInstance };