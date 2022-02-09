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
    DatabaseReference dbSkiingLeaderboardReference;
    DatabaseReference dbIndoorLeaderboardReference;
    DatabaseReference dbOutdoorLeaderboardReference;

    AuthManager authManager;

    private void Awake()
    {
        InitializeFirebase();
    }

    private void Start()
    {
        authManager = FindObjectOfType<AuthManager>();
    }

    public void InitializeFirebase()
    {
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("playerStats");
        dbPlayerLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");

        dbSkiingLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
        dbIndoorLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("indoorLeaderboard");
        dbOutdoorLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("outdoorLeaderboard");
    }

    // Updates the player's fastestTime, totalgame, and totalTime
    public void UpdatePlayerSkiiStats(string uuid, string displayname, int time)
    {
        Debug.Log("Entering.. update player stats" + uuid);
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        dbPlayerStatsReference.Child(uuid).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
            
                if (playerStats.Exists) //if there has been an entry created 
                    {
                    //Update player stats
                    //Compare existing highscore and set new highscore
                    //Create temp obj sp to store info from player stats

                    foreach(DataSnapshot d in playerStats.Children)
                    {
                        Debug.Log("snapshot value " + d.Key + ":" + d.Value);
                    }
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());
                    Debug.Log("sp data in update" + sp.SimplePlayerStatsToJson());

                    sp.totalGame += 1;
                    sp.totalTime += time;
                    sp.updatedOn = sp.GetTimeUnix();

                    // Check if there is a new highscore
                    // Update leaderboard if there is a new highscore
                    if (time > sp.fastestTime)
                    {
                        sp.fastestTime = time;
                        UpdatePlayerLeaderboardEntry(uuid, time, sp.updatedOn, Stats.Skiing);

                        Debug.Log("highscore updated!");
                    }

                    //Update with entire temp sp obj
                    //playerstats/$uuid/
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
                else
                {
                    Debug.Log("Let's write new entry" + uuid);

                    //Create player stats if no existing data, first time player
                    SimplePlayerStats sp = new SimplePlayerStats(displayname, time, Stats.Skiing);
                    SkiingLeaderboard lb = new SkiingLeaderboard(displayname, time);


                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    dbSkiingLeaderboardReference.Child(uuid).SetRawJsonValueAsync(lb.LeaderboardToJson());
                }

            }
        });
    }

    // Updates the player's outdoorPoints, outdoorTotalGame, and outdoorTotalPoints
    public void UpdatePlayerOutdoorStats(string uuid, string displayname, int points)
    {
        Debug.Log("Entering.. update player stats" + uuid);
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        dbPlayerStatsReference.Child(uuid).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;

                if (playerStats.Exists) //if there has been an entry created 
                {
                    //Update player stats
                    //Compare existing highscore and set new highscore
                    //Add xp per game
                    //Create temp obj sp to store info from player stats

                    foreach (DataSnapshot d in playerStats.Children)
                    {
                        Debug.Log("snapshot value " + d.Key + ":" + d.Value);
                    }
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());
                    Debug.Log("sp data in update" + sp.SimplePlayerStatsToJson());

                    sp.outdoorTotalGame += 1;
                    sp.outdoorTotalPoints += points;
                    sp.updatedOn = sp.GetTimeUnix();

                    // Check if there is a new highscore
                    // Update leaderboard if there is a new highscore
                    if (points > sp.outdoorPoints)
                    {
                        sp.outdoorPoints = points;
                        UpdatePlayerLeaderboardEntry(uuid, points, sp.updatedOn, Stats.OutdoorShooting);

                        Debug.Log("highscore updated!");
                    }

                    //Update with entire temp sp obj
                    //playerstats/$uuid/
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
                else
                {
                    Debug.Log("Let's write new entry" + uuid);

                    //Create player stats if no existing data, first time player
                    SimplePlayerStats sp = new SimplePlayerStats(displayname, points, Stats.OutdoorShooting);
                    OutdoorLeaderboard lb = new OutdoorLeaderboard(displayname, points);

                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    dbOutdoorLeaderboardReference.Child(uuid).SetRawJsonValueAsync(lb.LeaderboardToJson());
                }

            }
        });
    }

    // Updates the player's indoorPoints, indoorTotalGame, and indoorTotalPoints
    public void UpdatePlayerIndoorStats(string uuid, string displayname, int points)
    {
        Debug.Log("Entering.. update player stats" + uuid);
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        dbPlayerStatsReference.Child(uuid).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;

                if (playerStats.Exists) //if there has been an entry created 
                {
                    //Update player stats
                    //Compare existing highscore and set new highscore
                    //Create temp obj sp to store info from player stats

                    foreach (DataSnapshot d in playerStats.Children)
                    {
                        Debug.Log("snapshot value " + d.Key + ":" + d.Value);
                    }
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());
                    Debug.Log("sp data in update" + sp.SimplePlayerStatsToJson());

                    sp.indoorTotalGame += 1;
                    sp.indoorTotalPoints += points;
                    sp.updatedOn = sp.GetTimeUnix();

                    // Check if there is a new highscore
                    // Update leaderboard if there is a new highscore
                    if (points > sp.indoorPoints)
                    {
                        sp.indoorPoints = points;
                        UpdatePlayerLeaderboardEntry(uuid, points, sp.updatedOn, Stats.IndoorShooting);

                        Debug.Log("highscore updated!");
                    }

                    //Update with entire temp sp obj
                    //playerstats/$uuid/
                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
                else
                {
                    Debug.Log("Let's write new entry" + uuid);

                    //Create player stats if no existing data, first time player
                    SimplePlayerStats sp = new SimplePlayerStats(displayname, points, Stats.IndoorShooting);
                    IndoorLeaderboard lb = new IndoorLeaderboard(displayname, points);

                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    dbIndoorLeaderboardReference.Child(uuid).SetRawJsonValueAsync(lb.LeaderboardToJson());
                }

            }
        });
    }


    // Updates the fastestTime or the points of the specific game leaderboard if they have a new highscore
    public void UpdatePlayerLeaderboardEntry(string uuid, int score, long updatedOn, Stats statsType)
    {
        switch (statsType)
        {
            case Stats.Skiing:
                dbSkiingLeaderboardReference.Child(uuid).Child("fastestTime").SetValueAsync(score);
                dbSkiingLeaderboardReference.Child(uuid).Child("updatedOn").SetValueAsync(updatedOn);

                break;

            case Stats.IndoorShooting:
                dbIndoorLeaderboardReference.Child(uuid).Child("indoorPoints").SetValueAsync(score);
                dbIndoorLeaderboardReference.Child(uuid).Child("updatedOn").SetValueAsync(updatedOn);

                break;

            case Stats.OutdoorShooting:
                dbOutdoorLeaderboardReference.Child(uuid).Child("outdoorPoints").SetValueAsync(score);
                dbOutdoorLeaderboardReference.Child(uuid).Child("updatedOn").SetValueAsync(updatedOn);

                break;
        }
    }

    // Get the leaderboard data from the "leaderboard" entry in Firebase
    public async Task<List<SkiingLeaderboard>> GetSkiiLeaderboard(int limit = 5)
    {
        Query q = dbPlayerLeaderboardReference.OrderByChild("fastestTime").LimitToLast(limit);
        List<SkiingLeaderboard> leaderboardList = new List<SkiingLeaderboard>();

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
                        SkiingLeaderboard lb = JsonUtility.FromJson<SkiingLeaderboard>(d.GetRawJsonValue());

                        //Add item to list 
                        leaderboardList.Add(lb);
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.fastestTime);
                    }
                    //For each simpleleaderboard obj inside leaderboard list
                    foreach (SkiingLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.fastestTime);
                        rankCounter++;
                    }
                }
            }
        });

        return leaderboardList;
    }

    // Get the leaderboard data from the "outdoorLeaderboard" entry in Firebase
    public async Task<List<OutdoorLeaderboard>> GetOutdoorLeaderboard(int limit = 5)
    {
        Query q = dbOutdoorLeaderboardReference.OrderByChild("outdoorPoints").LimitToLast(limit);
        List<OutdoorLeaderboard> leaderboardList = new List<OutdoorLeaderboard>();

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
                        //Create temp obj based on results
                        OutdoorLeaderboard lb = JsonUtility.FromJson<OutdoorLeaderboard>(d.GetRawJsonValue());

                        //Add item to list 
                        leaderboardList.Add(lb);
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.outdoorPoints);
                    }
                    //For each simpleleaderboard obj inside leaderboard list
                    foreach (OutdoorLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.outdoorPoints);
                        rankCounter++;
                    }
                }
            }
        });

        return leaderboardList;
    }

    // Get the leaderboard data from the "indoorLeaderboard" entry in Firebase
    public async Task<List<IndoorLeaderboard>> GetIndoorLeaderboard(int limit = 5)
    {
        Query q = dbIndoorLeaderboardReference.OrderByChild("indoorPoints").LimitToLast(limit);
        List<IndoorLeaderboard> leaderboardList = new List<IndoorLeaderboard>();

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
                        //Create temp obj based on results
                        IndoorLeaderboard lb = JsonUtility.FromJson<IndoorLeaderboard>(d.GetRawJsonValue());

                        //Add item to list 
                        leaderboardList.Add(lb);
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.indoorPoints);
                    }
                    //For each simpleleaderboard obj inside leaderboard list
                    foreach (IndoorLeaderboard lb in leaderboardList)
                    {
                        Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Highscore {2}", rankCounter, lb.displayname, lb.indoorPoints);
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
