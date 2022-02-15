using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    //[SerializeField] protected GameObject _bulletHole;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected int _ammo;
    [SerializeField] protected GameObject _weaponBarrel;
    private bool _isEquipped;
    protected bool _canShoot = true;
    private WaitForSeconds _fireCountdown;

    /// <summary>
    /// Initial States
    /// </summary>
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private int _initialAmmo;
    
    /// <summary>
    /// Physics Stuff
    /// </summary>
    private RaycastHit hit;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialAmmo = _ammo;

        _fireCountdown = new WaitForSeconds(1 / _fireRate);
    }

    public bool IsEquipped
    {
        get
        {
            return _isEquipped;
        }

        set
        {
            _isEquipped = value;
        }
    }

    // Turn off motion and rotation constraints
    // Turn on gravity
    public void TurnOffRigidbodyConstraints()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;
    }

    public void TurnOnRigidbodyConstraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.useGravity = false;
    }

    public virtual void LeftClick()
    {
        //if (Input.GetButtonDown("Fire1") && _canShoot)
        //{
        //}
        _canShoot = false;
        StartCoroutine(FireRoutine());
        Shoot();
    }

    protected void Shoot()
    {
        if (_ammo > 0)
        {
            _ammo--;

            if (Physics.Raycast(_weaponBarrel.transform.position, _weaponBarrel.transform.forward, out hit))
            {
                TargetPart hitTarget = hit.collider.GetComponent<TargetPart>(); // The Target itself
                
                if (hitTarget != null)
                {
                    hitTarget.Hit();

                    GameObject bulletHoleInstance = Instantiate(Resources.Load<GameObject>("Bullet Hole"), hit.point, Quaternion.Euler(0, 180, 0), hitTarget.transform);
                    bulletHoleInstance.transform.position -= bulletHoleInstance.transform.forward / 1000;
                }
            }
        }
        else
        {
            RanOutOfEmmo();
        }
    }

    protected IEnumerator FireRoutine()
    {
        yield return _fireCountdown;
        _canShoot = true;
    }

    public void ResetWeaponState()
    {
        TurnOnRigidbodyConstraints();

        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _ammo = _initialAmmo;
    }

    private void RanOutOfEmmo() 
    {
        UIManager.DisplayMagazineError();
    }
}
