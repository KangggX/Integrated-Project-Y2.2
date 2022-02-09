using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private Transform _inPosition;
    [SerializeField] private Transform _outPosition;

    private float _outPositionZ = -17.675f;
    private float _inPositionZ = -1.8f;

    private Vector3 _positon1;
    private Vector3 _positon2;

    [SerializeField] private bool _isOut;
    [SerializeField] private bool _canMove;

    private Vector3 _basePosition;
    private float _elapsedTime;

    private void Start()
    {
        _basePosition = gameObject.transform.position;

        _positon1 = new Vector3(transform.localPosition.x, transform.localPosition.y, _inPositionZ);
        _positon2 = new Vector3(transform.localPosition.x, transform.localPosition.y, _outPositionZ);
    }

    private void Update()
    {
        //if (_isOut && _canMove)
        //{
        //    _elapsedTime += Time.deltaTime;
        //    float percentageCompletion = _elapsedTime / _transitionSpeed;

        //    Vector3 desiredPosition = new Vector3(_inPosition.position.x, _basePosition.y, _inPosition.position.z);

        //    gameObject.transform.position = Vector3.Lerp(_basePosition, desiredPosition, percentageCompletion);

        //    if (gameObject.transform.position == desiredPosition)
        //    {
        //        _isOut = false;
        //        _canMove = false;
        //        _elapsedTime = 0;

        //        _basePosition = gameObject.transform.position;
        //    }
        //}
        //else if (!_isOut && _canMove)
        //{
        //    _elapsedTime += Time.deltaTime;
        //    float percentageCompletion = _elapsedTime / _transitionSpeed;

        //    Vector3 desiredPosition = new Vector3(_outPosition.position.x, _basePosition.y, _outPosition.position.z);

        //    gameObject.transform.position = Vector3.Lerp(_basePosition, desiredPosition, percentageCompletion);

        //    if (gameObject.transform.position == desiredPosition)
        //    {
        //        _isOut = true;
        //        _canMove = false;
        //        _elapsedTime = 0;

        //        _basePosition = gameObject.transform.position;
        //    }
        //}

        if (!_isOut && _canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.localPosition = Vector3.Lerp(_positon1, _positon2, percentageCompletion);

            if (gameObject.transform.localPosition == _positon2)
            {
                _isOut = true;
                _canMove = false;
                _elapsedTime = 0;
            }
        }
    }

    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }
}
