using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutdoorLeaderboard
{
    public string displayname;
    public int outdoorPoints;
    public long updatedOn;

    public OutdoorLeaderboard()
    {

    }

    public OutdoorLeaderboard(string displayName, int points)
    {
        this.displayname = displayName;
        this.outdoorPoints = points;
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
