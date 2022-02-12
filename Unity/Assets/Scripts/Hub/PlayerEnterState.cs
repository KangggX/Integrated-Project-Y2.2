using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterState : MonoBehaviour
{
    public bool _hasEnteredBefore = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
