using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrel;
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
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * 1000, Color.green);

            if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward, out hit))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if ((hit.collider.gameObject.layer == 6) && (Input.GetButtonDown("Fire1")))
        {
            Debug.Log("Hit!");
        }
    }
}
