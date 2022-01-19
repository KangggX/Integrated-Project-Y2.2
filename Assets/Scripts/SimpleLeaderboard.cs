using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleLeaderboard
{
    public string displayname;
    public int fastestTime;
    public long updatedOn;
    
    //Constructor
    public SimpleLeaderboard()
    {

    }

    public SimpleLeaderboard(string displayname, int fastestTime)
    {
        this.displayname = displayname;
        this.fastestTime = fastestTime;
        this.updatedOn = GetTimeUnix();
    }

    public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }
    public string SimpleLeaderboardToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
