using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kang Xuan

Name of Class: Target

Description of Class: The main script that is behind every target in Indoor Shooting. Handles the movement and the collider.

Date Created: 18/02/2022
**/
public class Target : MonoBehaviour
{
    // Delegate event to check if current target point is hit -> update UI text
    public event Action<int> OnPointsChanged;

    private TargetManager _targetManager;

    [Header("Target Movement")]
    [SerializeField] private float _transitionSpeed;

    [Header("Target Hit Parts")]
    [SerializeField] private Transform[] _targetParts;
    private BoxCollider[] _targetPartsColliders;

    private int _totalPoints;
    private bool _inUse;

    private Vector3 _inPosition;
    private Vector3 _outPosition;
    private float _inPositionZ = 2f;
    private float _outPositionZ = -12.92406f;

    private bool _isOut;
    private bool _canMove;
    private bool _isMoving;
    private bool _moveIn;
    private bool _moveOut;

    private float _elapsedTime;

    private void Awake()
    {
        _targetPartsColliders = GetComponentsInChildren<BoxCollider>();
    }

    private void Start()
    {
        _targetManager = FindObjectOfType<TargetManager>();

        _inPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _inPositionZ);
        _outPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _outPositionZ);

        EnableTargetColliders(false);
    }

    private void Update()
    {
        if (CanMove)
        {
            // Elapsed time for the movement based on _elapsedTime variable
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            _isMoving = true;
            
            if (_moveIn) // If target should be moving in
            {
                gameObject.transform.localPosition = Vector3.Lerp(_outPosition, _inPosition, percentageCompletion);
                IsOut = false;

                if (gameObject.transform.localPosition == _inPosition)
                {
                    CanMove = false;
                    IsMoving = false;
                    _elapsedTime = 0;
                }
            }
            else if (_moveOut) // If target should be moving out
            {
                gameObject.transform.localPosition = Vector3.Lerp(_inPosition, _outPosition, percentageCompletion);

                if (gameObject.transform.localPosition == _outPosition)
                {
                    CanMove = false;
                    IsMoving = false;
                    _elapsedTime = 0;

                    IsOut = true; // Target colliders enabled
                }
            }
        }
    }

    public bool InUse
    {
        get { return _inUse; }
        set { _inUse = value; _targetManager.CheckTargetInUse(); }
    }

    public int TotalPoints
    {
        get { return _totalPoints; }
        set { _totalPoints = value; OnPointsChanged?.Invoke(_totalPoints); }
    }

    public bool CanMove
    {
        get { return _canMove; }
        set 
        { 
            _canMove = value; 

            if (_canMove)
            {
                _moveIn = _isOut; // If target is out, _moveIn = true, vice-versa
                _moveOut = !_isOut; // If target isn't out (aka in), _moveOut = true, vice-versa
            }
        }
    }

    // Property used to check if target is moving or not
    public bool IsMoving { 
        get { return _isMoving; } 
        private set { _isMoving = value; } 
    }

    private bool IsOut
    {
        get { return _isOut; }
        set { _isOut = value; EnableTargetColliders(_isOut); } 
    }

    // Allow target to be moved
    public void MoveTarget()
    {
        if (!CanMove)
        {
            CanMove = true;
        }
    }

    // If target is currently in the out position, move it back in (mainly for resetting the game)
    public void CheckIfTargetIsOut()
    {
        if (IsOut)
        {
            CanMove = true;
        }
    }

    // Enable/Disable the box colliders for each target parts based on the "state" parameter
    private void EnableTargetColliders(bool state)
    {
        foreach (var collider in _targetPartsColliders)
        {
            collider.enabled = state;
        }
    }

    // Clear bullet holes in the child of the target
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

    // Reset points
    public void ResetPoints()
    {
        TotalPoints = 0;
    }
}
