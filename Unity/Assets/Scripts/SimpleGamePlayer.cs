using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
Author: Aaron

Name of Class: SimpleGamePlayer

Description of Class: Object for creating user that is then sent to Firebase Database

Date Created: 18/02/2022
**/
public class SimpleGamePlayer
{
    public string username;
    public string displayname;
    public string email;
    public bool active;
    public long lastLoggedIn;
    public long createdOn;
    public long updatedOn;

    //Empty constructor
    public SimpleGamePlayer()
    {

    }

    public SimpleGamePlayer(string username, string displayname, string email, bool active = true)
    {
        this.username = username;
        this.displayname = displayname;
        this.email = email;
        this.active = active;

        //Timestamp properties
        var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        this.lastLoggedIn = timestamp;
        this.createdOn = timestamp;
        this.updatedOn = timestamp;
    }

    //Helper functions
    public string SimplerGamePlayerToJson()
    {
        return JsonUtility.ToJson(this);
    }

    //Print out player details
    public string PrintPlayer()
    {
        return string.Format("Player details: {0} \n Username: {1} \n Email: {2} \n Active: {3}", this.displayname, this.username, this.email, this.active);
    }
}
