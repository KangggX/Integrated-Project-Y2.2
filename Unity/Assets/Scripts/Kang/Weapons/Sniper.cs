using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    [Header("Sniper Settings")]
    [SerializeField] private float _fireRate;
    private bool _canShoot = true;

    public override void LeftClick()
    {
        if (Input.GetButtonDown("Fire1") && _canShoot)
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
