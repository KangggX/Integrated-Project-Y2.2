using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private GameObject _bulletHole;

    private RaycastHit hit;
    private GameObject _mainCamera;

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (gameObject.transform.parent != null)
        {
            //Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit))
                {
                    Shoot();
                }
            }
        }
    }

    private void Shoot()
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
