/*
Author: Lee Ka Meng Marcus

Name of Class: Timer

Description of Class: Recording the time used for completing the game.

Date Created: 10 / 02 / 2022
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    //gameobjects
    private GameManager _gameManager;
    private SwingMovement _swingMovement;

    [SerializeField] private GameObject _restartPrompt;

    //time variables
    public bool timeActive = false;
    public float currentTime;
    public TMP_Text currentTimeText;
    public bool isUpdated = false;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _swingMovement = GetComponent<SwingMovement>();

        _restartPrompt.SetActive(false);

        timeActive = false;
        currentTime = 0;
    }

    //detect start and end of game
    private void OnTriggerEnter(Collider time)
    {
        if(time.tag == "start")
        {
            StartTimer();
        }
        if(time.tag == "end")
        {
            GameEnd();
            _restartPrompt.SetActive(true);
        }
    }

    private void Update()
    {
        if (timeActive == true)
        {
            //record time when still in game
            currentTime = currentTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        }
        Debug.Log(timeActive);
    }

    //start and end of game trigger
    public void StartTimer()
    {
        timeActive = true;
    }
    public void GameEnd()
    {
        timeActive = false;
        
        _gameManager.UpdatePlayerSkiiStats((int)currentTime);
    }

    /*public void UpdatePlayerStats(int time)
    {
        fbMgr.UpdatePlayerStats(authMgr.GetCurrentUser().UserId, authMgr.GetCurrentUserDisplayName(), time);
    }*/
}
