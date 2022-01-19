using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayerStats
{

    public string displayname;
    public int fastestTime;
    public int totalGame;
    public int totalTime;
    public long updatedOn;

    //Constructor
    public SimplePlayerStats()
    {

    }

    public SimplePlayerStats(string displayname, int fastestTime, int totalGame = 0, int totalTime = 0)
    {
        this.displayname = displayname;
        this.fastestTime = fastestTime;
        this.totalGame = totalGame;
        this.totalTime = totalTime;

        var timestamp = this.GetTimeUnix();
        this.updatedOn = timestamp;
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
