using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Animator _animator;
    private GameObject _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerParentThis()
    {
        _player.transform.parent = this.gameObject.transform;
        _player.transform.localPosition = Vector3.zero;
    }
}
