using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterState : MonoBehaviour
{
    public static bool HasEnteredBefore = true;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
