using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameState : MonoBehaviour
{
    public static bool HasEnteredBefore = false;

    public static bool CanPlaySkiing = true;
    public static bool CanPlayIndoorShooting = true;
    public static bool CanPlayOutdoorShooting = true;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Skii " + CanPlaySkiing);
        Debug.Log("Indoor Shooting " + CanPlayIndoorShooting);
        Debug.Log("Outdoor Shooting " + CanPlayOutdoorShooting);
    }
}
