using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHub : MonoBehaviour
{
    [SerializeField] private Animator _elevatorAnimator;
    private PlayerEnterState _playerEnterState;

    private void Start()
    {
        _playerEnterState = FindObjectOfType<PlayerEnterState>();

        PlayerEntrancePosition();
    }

    // Determine where to spawn the player
    // Either spawn the player at the Entrance if it is their first time
    // Or spawn the player in the Elevator if it isn't their first time
    private void PlayerEntrancePosition()
    {
        if (_playerEnterState._hasEnteredBefore)
        {
            Debug.Log("Player has entered hub before!");
            SpawnPlayer(SpawnPosition.Elevator);
        }
        else
        {
            Debug.Log("This is the first time the player is entering the hub");
            _playerEnterState._hasEnteredBefore = true;
            SpawnPlayer(SpawnPosition.Entrance);
        }
    }

    // Spawning the player
    private void SpawnPlayer(SpawnPosition spawnPosition)
    {
        switch (spawnPosition)
        {
            case SpawnPosition.Entrance:

                break;

            case SpawnPosition.Elevator:

                break;
        }
    }
}

public enum SpawnPosition
{
    Entrance,
    Elevator
}
