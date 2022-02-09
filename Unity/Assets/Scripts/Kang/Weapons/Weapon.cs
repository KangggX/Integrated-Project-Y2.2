using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    //[SerializeField] protected GameObject _bulletHole;
    [SerializeField] protected float _fireRate;
    [SerializeField] protected int _ammo;
    private int _initialAmmo;
    private bool _isEquipped;
    protected bool _canShoot = true;
    private WaitForSeconds _fireCountdown;

    [Header("Weapon Scope Settings")]
    [SerializeField] protected GameObject _scopeOverlay;
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
        _fireCountdown = new WaitForSeconds(1 / _fireRate);
    }

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _initialScopeFOV = _mainCamera.GetComponent<Camera>().fieldOfView;
        _currFOV = _initialScopeFOV;
    }

    public virtual void Update()
    {
        if (IsEquipped)
        {
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);
            
            _mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(_mainCamera.GetComponent<Camera>().fieldOfView, _currFOV, Time.deltaTime * _lerpSpeed);

            LeftClick();
            RightClick();
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

    public virtual void LeftClick()
    {
        if (Input.GetButtonDown("Fire1") && _canShoot)
        {
            _canShoot = false;
            StartCoroutine(FireRoutine());
            Shoot();
        }
    }

    public virtual void RightClick()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Scope();
        }
    }

    protected void Shoot()
    {
        if (_ammo > 0)
        {
            _ammo--;

            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit))
            {
                Target hitTarget = hit.collider.GetComponent<Target>(); // The Target itself
                
                if (hitTarget != null)
                {
                    hitTarget.Hit();

                    GameObject bulletHoleInstance = Instantiate(Resources.Load<GameObject>("Bullet Hole"), hit.point, Quaternion.Euler(0, 180, 0), hitTarget.transform);
                    bulletHoleInstance.transform.position -= bulletHoleInstance.transform.forward / 1000;

                    /*Destroy(bulletHoleInstance, 2);*/ // Delete the bullet hole instance after 2s
                }
            }
        }
        else
        {
            RanOutOfEmmo();
        }
    }

    public virtual void Scope()
    {
        _isScoped = !_isScoped;

        if (_isScoped)
        {
            StartCoroutine(OnScope());
        }
        else
        {
            StartCoroutine(OnDescope());
        }
    }

    private IEnumerator OnScope()
    {
        if (_scopeOverlay != null)
        {
            _animator.SetBool("isScoped", _isScoped); // Activate the gun's scoped animation

            yield return new WaitForSeconds(0.25f);
            _currFOV = _scopeFOV; // Zoom the camera in when scoped

            if (transform.childCount > 0)
            {
                foreach (Transform parts in transform)
                {
                    parts.gameObject.SetActive(!_isScoped); // Disable the gun parts when scoped (to maintain immersiveness)
                }
            }

            _scopeOverlay.SetActive(_isScoped); // Turn on the zoom overlay
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            _currFOV = _scopeFOV; // Zoom the camera in when scoped
        }
    }

    private IEnumerator OnDescope()
    {
        if (_scopeOverlay != null)
        {
            _animator.SetBool("isScoped", _isScoped); // Deactivate the gun's scope animation

            if (transform.childCount > 0)
            {
                foreach (Transform parts in transform)
                {
                    parts.gameObject.SetActive(!_isScoped); // Enable the gun parts when descoped (to maintain immersiveness)
                }
            }

            _scopeOverlay.SetActive(_isScoped); // Turn off the zoom overlay
            _currFOV = _initialScopeFOV; // Reset camera zoom to OG

            yield return null;
        }
        else
        {
            _currFOV = _initialScopeFOV; // Reset camera zoom to OG

            yield return null;
        }
    }

    protected IEnumerator FireRoutine()
    {
        yield return _fireCountdown;
        _canShoot = true;
    }

    public void ResetAmmo()
    {
        _ammo = _initialAmmo;
    }

    private void RanOutOfEmmo() 
    {
        UIManager.DisplayMagazineError();
    }
}
