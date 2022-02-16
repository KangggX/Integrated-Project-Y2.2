using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameState : MonoBehaviour
{
    public static bool HasEnteredBefore = false;

    public static bool CanPlaySkiing;
    public static bool CanPlayIndoorShooting = true;
    public static bool CanPlayOutdoorShooting;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
