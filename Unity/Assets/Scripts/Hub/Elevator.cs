using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    private Animator _animator;
    private MeshCollider _meshCollider;
    private GameObject _player;

    private string _currScene;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        // Assignt the Mesh Collider that is a trigger to _meshCollider
        foreach (MeshCollider collider in GetComponents<MeshCollider>())
        {
            if (collider.isTrigger)
            {
                _meshCollider = collider;
            }
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _currScene = SceneManager.GetActiveScene().name;
    }

    private void OnTriggerEnter(Collider other)
    {
        ParentPlayer();
        EnableUsagePrompt(true);
    }

    // Unparent the player from the elevator
    // Disable Mesh Collider afterwards to save some performance
    private void OnTriggerExit(Collider other)
    {
        UnparentPlayer();
        EnableUsagePrompt(false);
        
        // If the current scene is "Hub"
        if (_currScene == "Hub")
        {
            _meshCollider.enabled = false;
        }
    }

    // Spawn player in the elevator
    // Restrict player movement (can still rotate)
    // Play the "Go Down" animation
    public void ElevatorSpawn()
    {
        ParentPlayer();
        LockPlayerMovement();
        _player.transform.localPosition = Vector3.zero;

        GoDownAnim();
    }

    // Parent the player to the elevator
    // Mainly to ensure that player follows the elevator position when going down
    private void ParentPlayer()
    {
        _player.transform.SetParent(transform, true);
    }

    // Unparent the player from the elevator
    private void UnparentPlayer()
    {
        _player.transform.SetParent(null, true);
    }

    // Restrict player from moving
    private void LockPlayerMovement()
    {
        PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();

        playerMovement.speed = 0;
    }

    // Enable player to move
    private void UnlockPlayerMovement()
    {
        PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();

        playerMovement.speed = playerMovement.initialSpeed; 
    }

    private void EnableUsagePrompt(bool state)
    {
        if (_currScene != "Hub")
        {
            if (state)
            {
                Debug.Log("Hi");
            }
            else
            {
                Debug.Log("Hi2");
            }
        }
    }

    // Transition to hub scene
    private void LoadHubScene()
    {
        SceneManager.LoadScene(1);
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
