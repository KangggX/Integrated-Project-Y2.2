using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorLeaderboard
{
    public string displayname;
    public int indoorPoints;
    public long updatedOn;

    public IndoorLeaderboard()
    {

    }

    public IndoorLeaderboard(string displayName, int points)
    {
        this.displayname = displayName;
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
