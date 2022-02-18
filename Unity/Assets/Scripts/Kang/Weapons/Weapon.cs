using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
Author: Kang Xuan

Name of Class: Weapon

Description of Class: Main script where all weapons in Indoor Shooting derives from

Date Created: 18/02/2022
**/
public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] protected float _fireRate;
    [SerializeField] protected int _ammo;
    [SerializeField] protected TextMeshProUGUI _ammoText;
    [SerializeField] protected GameObject _weaponBarrel;
    private bool _isEquipped;
    private bool _triggerPulled;
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

    private void Start()
    {
        TurnOnRigidbodyConstraints();

        _ammoText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (TriggerPulled && _canShoot)
        {
            _canShoot = false;
            StartCoroutine(FireRoutine());

            Shoot();
        }

        if (IsEquipped)
        {
            _ammoText.gameObject.SetActive(IsEquipped);

            _ammoText.text = _ammo.ToString();
        }
        else
        {
            _ammoText.gameObject.SetActive(IsEquipped);
        }
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

    public bool TriggerPulled
    {
        get { return _triggerPulled; }
        set { _triggerPulled = value; }
    }

    // Turn off motion and rotation constraints
    // Turn on gravity
    public void TurnOffRigidbodyConstraints()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;
    }

    // Turn on motion and rotation constraints
    // Turn off gravity
    public void TurnOnRigidbodyConstraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezeAll;
        _rb.useGravity = false;
    }

    // Function to shoot
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
    }

    // Coroutine that enabled _canShoot based on _fireCountdown seconds
    protected IEnumerator FireRoutine()
    {
        yield return _fireCountdown;
        _canShoot = true;
    }

    // Reset weapon to original position and ammo amount
    public void ResetWeaponState()
    {
        TurnOnRigidbodyConstraints();

        transform.position = _initialPosition;
        transform.rotation = _initialRotation;
        _ammo = _initialAmmo;
    }
}
