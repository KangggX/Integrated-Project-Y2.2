using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    private GameManager _gameManager;
    private SwingMovement _swingMovement;

    public bool timeActive = false;
    public float currentTime;
    public TMP_Text currentTimeText;
    public bool isUpdated = false;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _swingMovement = GetComponent<SwingMovement>();

        timeActive = false;
        currentTime = 0;
    }

    private void OnTriggerEnter(Collider time)
    {
        if(time.tag == "start")
        {
            StartTimer();
        }
        if(time.tag == "end")
        {
            GameEnd();
        }
    }

    private void Update()
    {
        if (timeActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        }
        Debug.Log(timeActive);
    }
    public void StartTimer()
    {
        timeActive = true;
    }
    public void GameEnd()
    {
        timeActive = false;

        _gameManager.UpdatePlayerSkiiStats((int)currentTime);
        _swingMovement.maxToZeroSki = 1;
    }

    /*public void UpdatePlayerStats(int time)
    {
        fbMgr.UpdatePlayerStats(authMgr.GetCurrentUser().UserId, authMgr.GetCurrentUserDisplayName(), time);
    }*/
}
