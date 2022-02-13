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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject == _test)
    //    {
    //        _test.transform.parent = transform;
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        UnparentPlayer();
    }

    public void ElevatorSpawn()
    {
        ParentPlayer();
        _player.transform.localPosition = Vector3.zero;

        GoDownAnim();
    }

    public void ParentPlayer()
    {
        _player.transform.parent = this.gameObject.transform;
    }

    public void UnparentPlayer()
    {
        _player.transform.SetParent(null, true);
    }

    private void GoDownAnim()
    {
        _animator.SetTrigger("Going Down");
    }

    private void GoUpAnim()
    {
        _animator.SetTrigger("Going Up");
    }
}
