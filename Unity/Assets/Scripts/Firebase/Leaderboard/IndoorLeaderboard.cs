using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorLeaderboard
{
    public string displayName;
    public int indoorPoints;
    public long updatedOn;

    public IndoorLeaderboard()
    {

    }

    public IndoorLeaderboard(string displayName, int points)
    {
        this.displayName = displayName;
        this.indoorPoints = points;
        this.updatedOn = GetTimeUnix();
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
    public string LeaderboardToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
