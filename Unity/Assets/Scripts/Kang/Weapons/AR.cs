using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR : Weapon
{
    [Header("AR Settings")]
    [SerializeField] private float _fireRate;
    private bool _canShoot = true;

    public override void LeftClick()
    {
        //if (Input.GetButton("Fire1") && Time.time >= _nextTimeToFire)
        //{
        //    _nextTimeToFire = Time.time + (1 / _fireRate);
        //    Shoot();
        //}

        if (Input.GetButton("Fire1") && _canShoot)
        {
            _canShoot = false;
            StartCoroutine(FireRoutine());  
            Shoot();
        }
    }

    private IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(1 / _fireRate);
        _canShoot = true;
    }
}
