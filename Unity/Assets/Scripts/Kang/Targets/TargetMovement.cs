using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    //[SerializeField] private float _transitionSpeed;

    //private float _inPositionZ = 2f;
    //private float _outPositionZ = -12.92406f;

    //private Vector3 _inPosition;
    //private Vector3 _outPosition;

    //private bool _isOut;
    //private bool _canMove;
    //private bool _isMoving;

    //private float _elapsedTime;

    //private void Start()
    //{
    //    _inPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _inPositionZ);
    //    _outPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _outPositionZ);
    //}

    //private void Update()
    //{
    //    if (_isOut && _canMove)
    //    {
    //        _elapsedTime += Time.deltaTime;
    //        float percentageCompletion = _elapsedTime / _transitionSpeed;

    //        _isMoving = true;


    //        gameObject.transform.localPosition = Vector3.Lerp(_outPosition, _inPosition, percentageCompletion);

    //        if (gameObject.transform.localPosition == _inPosition)
    //        {
    //            CanMove = false;
    //            _isMoving = false;
    //            _isOut = false;
    //            _elapsedTime = 0;
    //        }
    //    }
    //    else if (!_isOut && _canMove)
    //    {
    //        _elapsedTime += Time.deltaTime;
    //        float percentageCompletion = _elapsedTime / _transitionSpeed;

    //        _isMoving = true;


    //        gameObject.transform.localPosition = Vector3.Lerp(_inPosition, _outPosition, percentageCompletion);

    //        if (gameObject.transform.localPosition == _outPosition)
    //        {
    //            CanMove = false;
    //            _isMoving = false;
    //            _isOut = true;
    //            _elapsedTime = 0;
    //        }
    //    }
    //}

    //public bool CanMove
    //{
    //    get { return _canMove; }
    //    set { _canMove = value; }
    //}

    //public void MoveTarget()
    //{
    //    if (!CanMove)
    //    {
    //        CanMove = true;
    //    }
    //}

    //public bool IsMoving()
    //{
    //    return _isMoving;
    //}

    //public void CheckIfTargetIsOut()
    //{
    //    if (_isOut)
    //    {
    //        CanMove = true;
    //    }
    //}
}
