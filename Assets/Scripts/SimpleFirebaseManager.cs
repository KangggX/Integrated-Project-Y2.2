using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading.Tasks;

public class SimpleFirebaseManager : MonoBehaviour
{

    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbPlayerLeaderboardReference;

    AuthManager authMgr;

    private void Awake()
    {
        InitializeFirebase();
        
    }

    public void InitializeFirebase()
    {
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        dbPlayerLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
    }

    public void UpdatePlayerStats(string uuid, string displayname, int time)
    {
        Debug.Log("Entering.. update player stats" + uuid);
        Query playerQuery = dbPlayerStatsReference.Child(uuid);
        //Query playerQuery = dbPlayerLeaderboardReference.Child(uuid);

        //Read data first too check if there has been an entry using UUID
        //FirebaseDatabase.DefaultInstance.GoOnline();
        dbPlayerStatsReference.Child(uuid).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                //if there has been an entry created 
                 if (playerStats.Exists)
                 {
                    //Update player stats
                    //Compare existing highscore and set new highscore
                    //Add xp per game
                    //Create temp obj sp to store info from player stats

                    foreach(DataSnapshot d in playerStats.Children)
                    {
                        Debug.Log("snapshot value " + d.Key + ":" + d.Value);
                    }
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());
                    Debug.Log("sp data in update" + sp.SimplePlayerStatsToJson());
                    sp.totalGame = sp.totalGame + 1;
                    //sp.totalTime = sp.totalTime + (int)timer.currentTime;
                    sp.updatedOn = sp.GetTimeUnix();
                    //Check if there is a new highscore
                    //Update leaderboard if there is a new highscore\
                    if (time > sp.fastestTime)
                    {
                        sp.fastestTime = time;
                        UpdatePlayerLeaderboardEntry(uuid, sp.fastestTime, sp.updatedOn);
                        Debug.Log("highscore updated!");

                    }
                

                    //Update with entire temp sp obj
                    //playerstats/$uuid/
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
               else
                {
                    //Create player stats
                    //If no existing data, first time player
                    SimplePlayerStats sp = new SimplePlayerStats(displayname, time);

                    SimpleLeaderboard lb = new SimpleLeaderboard(displayname, time);
                    Debug.Log("Let's write new entry" + uuid);

                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    dbPlayerLeaderboardReference.Child(uuid).SetRawJsonValueAsync(lb.SimpleLeaderboardToJson());
                }

            }
        });
       // FirebaseDatabase.DefaultInstance.GoOffline();
    }

    public void UpdatePlayerLeaderboardEntry(string uuid, int fastestTime, long updatedOn)
    {
        //Update specific single entries

        //leaderboards/$uuid/highscore
        //leaderboards/$uuid/updatedOn
        dbPlayerLeaderboardReference.Child(uuid).Child("fastestTime").SetValueAsync(fastestTime);
        dbPlayerLeaderboardReference.Child(uuid).Child("updatedOn").SetValueAsync(updatedOn);
    }

    public async Task<List<SimpleLeaderboard>> GetLeaderboard(int limit = 5)
    {
        Query q = dbPlayerLeaderboardReference.OrderByChild("fastestTime").LimitToLast(limit);
        List<SimpleLeaderboard> leaderboardList = new List<SimpleLeaderboard>();

        await q.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error getting leaderboard entries, : ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                if (ds.Exists)
                {
                    int rankCounter = 1;
                    foreach (DataSnapshot d in ds.Children)
                    {
                        Debug.Log("yes");
                        //Create temp obj based on results
                        SimpleLeaderboard lb = JsonUtility.FromJson<SimpleLeaderboard>(d.GetRawJsonValue());

                        //Add item to list 
                        leaderboardList.Add(lb);
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.fastestTime);
                    }
                    //For each simpleleaderboard obj inside leaderboard list
                    foreach (SimpleLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.fastestTime);
                        rankCounter++;
                    }
                }
            }
        });
        return leaderboardList;
    }

    public async Task<SimplePlayerStats> GetPlayerStats(string uuid)
    {
        Query q = dbPlayerStatsReference.Child(uuid);
        SimplePlayerStats playerStats = null;

        await q.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error receiving your player stats. ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot ds = task.Result;

                //if (ds.Child(uuid).Exists)
                //{
                    playerStats = JsonUtility.FromJson<SimplePlayerStats>(ds.GetRawJsonValue());

                //}
                
            }
        });

        return playerStats;
    }


    public void DeletePlayerStats(string uuid)
    {
        dbPlayerStatsReference.Child(uuid).RemoveValueAsync();
        dbPlayerLeaderboardReference.Child(uuid).RemoveValueAsync();
    }
}
