using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kirdesh

Name of Class: PlayerGameState

Description of Class: Script that persists throughout scene transition that stores bool

Date Created: 18/02/2022
**/
public class PlayerGameState : MonoBehaviour
{
    public static bool HasEnteredBefore = false;

    public static bool CanPlaySkiing = false;
    public static bool CanPlayIndoorShooting = true;
    public static bool CanPlayOutdoorShooting = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Skii " + CanPlaySkiing);
        Debug.Log("Indoor Shooting " + CanPlayIndoorShooting);
        Debug.Log("Outdoor Shooting " + CanPlayOutdoorShooting);
    }
}
