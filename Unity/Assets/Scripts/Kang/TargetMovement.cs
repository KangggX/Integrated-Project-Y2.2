using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private Transform _inPosition;
    [SerializeField] private Transform _outPosition;

    [SerializeField] private bool _isOut;
    private bool _canMove;
    public bool CanMove
    {
        get { return _canMove; }
        set { _canMove = value; }
    }

    private Vector3 _basePosition;
    private float _elapsedTime;

    private void Start()
    {
        _basePosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (_isOut && _canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.position = Vector3.Lerp(_basePosition, _inPosition.position, percentageCompletion);

            if (gameObject.transform.position == _inPosition.position)
            {
                _isOut = false;
                _canMove = false;
                _elapsedTime = 0;

                _basePosition = gameObject.transform.position;
            }
        }
        else if (!_isOut && _canMove)
        {
            _elapsedTime += Time.deltaTime;
            float percentageCompletion = _elapsedTime / _transitionSpeed;

            gameObject.transform.position = Vector3.Lerp(_basePosition, _outPosition.position, percentageCompletion);

            if (gameObject.transform.position == _outPosition.position)
            {
                _isOut = true;
                _canMove = false;
                _elapsedTime = 0;

                _basePosition = gameObject.transform.position;
            }
        }
    }
}
