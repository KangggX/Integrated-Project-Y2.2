using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHub : MonoBehaviour
{
    [SerializeField] private Elevator _elevator;

    private void Start()
    {
        PlayerEntrancePosition();
    }

    // Determine where to spawn the player
    // Either spawn the player at the Entrance if it is their first time
    // Or spawn the player in the Elevator if it isn't their first time
    private void PlayerEntrancePosition()
    {
        if (PlayerEnterState.HasEnteredBefore)
        {
            Debug.Log("Player has entered hub before!");

            SpawnPlayer(SpawnPosition.Elevator);
        }
        else
        {
            Debug.Log("This is the first time the player is entering the hub");

            PlayerEnterState.HasEnteredBefore = true;
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
                //_player.transform.parent = _elevator.transform;
                //_player.transform.localPosition = Vector3.zero;

                //elevatorAnimator.SetTrigger("Going Down");
                _elevator.ElevatorSpawn();

                break;
        }
    }
}

public enum SpawnPosition
{
    Entrance,
    Elevator
}
