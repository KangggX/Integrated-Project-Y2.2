using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{

    public bool timeActive = false;
    public float currentTime;
    public TMP_Text currentTimeText;

    private void Start()
    {
        timeActive = false;
        currentTime = 0;
    }
    public bool isUpdated = false;

    //Firebase
    //public AuthManager authMgr;
    //public SimpleFirebaseManager fbMgr;


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
        if (isUpdated == false)
        {
            //UpdatePlayerStats((int)currentTime);
            isUpdated = true;
            //SceneManager.LoadScene("Main Menu");
        }
    }

    /*public void UpdatePlayerStats(int time)
    {
        fbMgr.UpdatePlayerStats(authMgr.GetCurrentUser().UserId, authMgr.GetCurrentUserDisplayName(), time);
    }*/
}
