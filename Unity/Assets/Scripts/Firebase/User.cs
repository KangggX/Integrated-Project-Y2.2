using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string name;
    public string email;
    public string displayname;

    public User(string username, string email, string displayname)
    {
        this.name = username;
        this.email = email;
        this.displayname = displayname;
    }
}