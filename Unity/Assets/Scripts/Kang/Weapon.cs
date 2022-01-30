using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : WeaponPositioning
{
    [Header("Weapon Parameter")]
    [SerializeField] protected Transform _gunBarrel;
    [SerializeField] protected GameObject _bulletHole;
    [SerializeField] protected int _ammo;

    private RaycastHit hit;
    private GameObject _mainCamera;
    private int _initialAmmo;

    private void Awake()
    {
        _initialAmmo = _ammo;
    }

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public override void Update()
    {
        base.Update();

        if (gameObject.transform.parent != null)
        {
            //Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);
            
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (_ammo > 0)
        {
            _ammo--;

            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit))
            {
                Target hitTarget = hit.collider.GetComponent<Target>(); // The Target itself
                
                if (hitTarget != null)
                {
                    hitTarget.GainPoint();

                    GameObject bulletHoleInstance = Instantiate(_bulletHole, hit.point, Quaternion.identity, hitTarget.transform);
                    bulletHoleInstance.transform.position -= bulletHoleInstance.transform.forward / 1000;

                    /*Destroy(bulletHoleInstance, 2);*/ // Delete the bullet hole instance after 2s
                }
            }
        }
        else
        {
            UIManager.DisplayMagazineError();
        }
    }

    private void Reload()
    {
        _ammo = _initialAmmo;
    }
}
