using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHub : MonoBehaviour
{
    private PlayerEnterState _playerEnterState;

    private void Start()
    {
        _playerEnterState = FindObjectOfType<PlayerEnterState>();

        PlayerEntrancePosition();
    }

    private void PlayerEntrancePosition()
    {
        if (_playerEnterState._hasEntered)
        {
            Debug.Log("Player has entered hub before!");
        }
        else
        {
            Debug.Log("This is the first time the player is entering the hub");
            _playerEnterState._hasEntered = true;
            SceneManager.LoadScene(1);
        }
    }
}
