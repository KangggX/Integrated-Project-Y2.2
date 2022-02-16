using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterState : MonoBehaviour
{
    public static bool HasEnteredBefore = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
