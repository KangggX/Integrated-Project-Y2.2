using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : Weapon
{
    public override void LeftClick()
    {
        if (Input.GetButton("Fire1") && _canShoot)
        {
            _canShoot = false;
            StartCoroutine(FireRoutine());  
            Shoot();
        }
    }
}
