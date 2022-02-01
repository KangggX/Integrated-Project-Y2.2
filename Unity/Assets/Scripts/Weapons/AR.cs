using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : Weapon
{
    [Header("AR Settings")]
    [SerializeField] private float _fireRate;
    private float _nextTimeToFire = 0f;

    public override void LeftClick()
    {
        if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + (1 / _fireRate);
            Shoot();
        }
    }
}
