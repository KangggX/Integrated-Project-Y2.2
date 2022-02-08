using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class SimpleLeaderboardManager : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform tableContents;

    private SimpleFirebaseManager firebaseManager;

    // Start is called before the first frame update
    void Start()
    {
        firebaseManager = FindObjectOfType<SimpleFirebaseManager>();
        UpdateLeaderboardUI();
    }

    public async void UpdateLeaderboardUI()
    {
        var leaderboardList = await firebaseManager.GetLeaderboard();
        int rankCounter = 1;

        //Clear all leaderboard entries in UI
        foreach (Transform item in tableContents)
        {
            Destroy(item.gameObject);
        }

        //Create prefabs of rows
        //Assign each value from list to the prefab text content
        foreach (SimpleLeaderboard lb in leaderboardList)
        {
            Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.fastestTime);

            //Create prefabs in the position of tableContents
            GameObject entry = Instantiate(rowPrefab, tableContents);
            TextMeshProUGUI[] leaderboardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();
            leaderboardDetails[0].text = rankCounter.ToString();
            leaderboardDetails[1].text = lb.displayname;
            TimeSpan t = TimeSpan.FromSeconds(lb.fastestTime);
            leaderboardDetails[2].text = t.Minutes.ToString() + ":" + t.Seconds.ToString();

            rankCounter++;
        }
    }
    
    public void MainMenu()
    {
        AudioClipManager.PlaySound("button");
        SceneManager.LoadScene(1);
    }

}