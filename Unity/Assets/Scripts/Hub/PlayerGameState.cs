using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameState : MonoBehaviour
{
    public static bool HasEnteredBefore = true;

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
