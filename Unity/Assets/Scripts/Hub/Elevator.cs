using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Animator _animator;
    private MeshCollider _meshCollider;
    private GameObject _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _meshCollider = GetComponent<MeshCollider>();
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

    // Unparent the player from the elevator
    // Disable Mesh Collider afterwards to save some performance
    private void OnTriggerExit(Collider other)
    {
        UnparentPlayer();
        _meshCollider.enabled = false;
    }

    // Spawn player in the elevator
    // Play the "Go Down" animation
    public void ElevatorSpawn()
    {
        ParentPlayer();
        _player.transform.localPosition = Vector3.zero;

        GoDownAnim();
    }

    // Parent the player to the elevator
    // Mainly to ensure that player follows the elevator position when going down
    public void ParentPlayer()
    {
        _player.transform.parent = this.gameObject.transform;
    }

    // Unparent the player from the elevator
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
