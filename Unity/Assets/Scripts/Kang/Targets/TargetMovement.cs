using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;

    private float _inPositionZ = -1.8f;
    private float _outPositionZ = -17.675f;

    private Vector3 _inPosition;
    private Vector3 _outPosition;

    [SerializeField] private bool _isOut;
    [SerializeField] private bool _canMove;

    private Vector3 _basePosition;
    private float _elapsedTime;

    private void Start()
    {
        _basePosition = gameObject.transform.position;

        _inPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _inPositionZ);
        _outPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _outPositionZ);
    }

    private void Update()
    {
        if (_isOut && _canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.localPosition = Vector3.Lerp(_outPosition, _inPosition, percentageCompletion);

            if (gameObject.transform.localPosition == _inPosition)
            {
                _isOut = false;
                _canMove = false;
                _elapsedTime = 0;
            }
        }
        else if (!_isOut && _canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.localPosition = Vector3.Lerp(_inPosition, _outPosition, percentageCompletion);

            if (gameObject.transform.localPosition == _outPosition)
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
