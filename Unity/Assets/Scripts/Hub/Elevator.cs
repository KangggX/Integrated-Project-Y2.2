using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    private Animator _animator;
    private MeshCollider _meshCollider;
    private GameObject _player;

    [SerializeField] private ElevatorType _elevatorType;
    [SerializeField] private GameObject _usagePrompt;
    private string _currScene;

    private void Awake()
    {
        // Get Animator component
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

    // Show prompt to use elevator
    private void EnableUsagePrompt(bool state)
    {
        if (_currScene != "Hub")
        {
            if (_usagePrompt != null)
            {
                _usagePrompt.SetActive(state);
            }
            else
            {
                Debug.Log("No Usage Prompt found");
            }
        }
    }

    // Enable PlayerGameState for Skii
    private void EnableSkiiState()
    {
        PlayerGameState.CanPlaySkiing = true;
    }

    // Enable PlayerGameState for Outdoor Shooting
    private void EnableOutdoorShootingState()
    {
        PlayerGameState.CanPlayOutdoorShooting = true;
    }

    // Transition to hub scene
    private void LoadHubScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GoDownAnim()
    {
        LockPlayerMovement();
        _animator.SetTrigger("Going Down");
    }

    private void GoUpAnim()
    {
        _animator.SetTrigger("Going Up");
    }
}

public enum ElevatorType
{
    Hub,
    Skiing,
    IndoorShooting,
    OutdoorShooting
}
