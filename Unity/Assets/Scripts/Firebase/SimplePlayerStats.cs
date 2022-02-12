using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayerStats
{
    public string displayname;

    // Skiing
    public int fastestTime = 0;
    public int totalTime = 0;
    public int totalGame = 0;

    // Outdoor Shooting
    public int outdoorPoints = 0;
    public int outdoorTotalPoints = 0;
    public int outdoorTotalGame = 0;
    
    // Indoor Shooting
    public int indoorPoints = 0;
    public int indoorTotalPoints = 0;
    public int indoorTotalGame = 0;

    public long updatedOn;

    //Constructor
    public SimplePlayerStats()
    {

    }

    public SimplePlayerStats(string displayname, int score, Stats statsType)
    {
        this.displayname = displayname;

        var timestamp = this.GetTimeUnix();
        this.updatedOn = timestamp;
        
        switch (statsType)
        {
            case (Stats.Skiing):
                this.fastestTime = score;
                this.totalTime = score;
                this.totalGame = 1;
                
                break;

            case (Stats.OutdoorShooting):
                this.outdoorPoints = score;
                this.outdoorTotalPoints = score;
                this.outdoorTotalGame = 1;

                break;

            case (Stats.IndoorShooting):
                this.indoorPoints = score;
                this.indoorTotalPoints = score;
                this.indoorTotalGame = 1;

                break;
        }
        
        //this.fastestTime = fastestTime;

    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }

    public string SimplePlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
