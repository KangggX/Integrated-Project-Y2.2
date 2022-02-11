using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiingLeaderboard
{
    public string displayname;
    public int fastestTime;
    public long updatedOn;

    public SkiingLeaderboard()
    {

    }

    public SkiingLeaderboard(string displayName, int fastestTime)
    {
        this.displayname = displayName;
        this.fastestTime = fastestTime;
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
