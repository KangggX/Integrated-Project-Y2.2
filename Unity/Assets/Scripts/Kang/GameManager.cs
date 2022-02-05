using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Weapon[] _weaponList;

    private void Start()
    {
        _weaponList = FindObjectsOfType<Weapon>();
    }

    public void ResetCurrentGame()
    {
        foreach (Weapon weapon in _weaponList)
        {
            weapon.ResetAmmo();
        }
    }
}
