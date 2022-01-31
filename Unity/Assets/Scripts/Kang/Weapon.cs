using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : WeaponPositioning
{
    [Header("Weapon Settings")]
    [SerializeField] protected GameObject _scopeOverlay;
    [SerializeField] protected Transform _gunBarrel;
    [SerializeField] protected GameObject _bulletHole;
    [SerializeField] protected int _ammo;
    private int _initialAmmo;
    private bool _isEquipped;

    [Header("Weapon Scope Settings")]
    [SerializeField] protected float _scopeFOV;
    [SerializeField] protected float _lerpSpeed;
    private float _initialScopeFOV;
    private float _currFOV;

    [Header("Animator Settings")]
    [SerializeField] protected Animator _animator;
    private bool _isScoped = false;

    private RaycastHit hit;
    private GameObject _mainCamera;

    private void Awake()
    {
        _initialAmmo = _ammo;
    }

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _initialScopeFOV = _mainCamera.GetComponent<Camera>().fieldOfView;
        _currFOV = _initialScopeFOV;
    }

    public override void Update()
    {
        base.Update();

        if (IsEquipped)
        {
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);
            
            _mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(_mainCamera.GetComponent<Camera>().fieldOfView, _currFOV, Time.deltaTime * _lerpSpeed);

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Scope();
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

    public virtual void Scope()
    {
        _isScoped = !_isScoped;

        if (_isScoped)
        {
            _animator.SetBool("isScoped", _isScoped); // Activate the gun's scoped animation
            StartCoroutine(OnScope());
        }
        else
        {
            _animator.SetBool("isScoped", _isScoped); // Deactivate the gun's scope animation
            StartCoroutine(OnDescope());
        }
    }

    private IEnumerator OnScope()
    {
        yield return new WaitForSeconds(0.25f);
        _currFOV = _scopeFOV; // Zoom the camera in when scoped
        
        if (_scopeOverlay != null)
        {
            if (transform.childCount > 0)
            {
                foreach (Transform parts in transform)
                {
                    parts.gameObject.SetActive(!_isScoped); // Disable the gun parts when scoped (to maintain immersiveness)
                }
            }

            _scopeOverlay.SetActive(_isScoped); // Turn on/off the zoom overlay
        }
    }

    private IEnumerator OnDescope()
    {
        if (_scopeOverlay != null)
        {
            if (transform.childCount > 0)
            {
                foreach (Transform parts in transform)
                {
                    parts.gameObject.SetActive(!_isScoped); // Enable the gun parts when descoped (to maintain immersiveness)
                }
            }

            _scopeOverlay.SetActive(_isScoped); // Turn on/off the zoom overlay
        }
        _currFOV = _initialScopeFOV; // Reset camera zoom to OG

        yield return null;
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
}
