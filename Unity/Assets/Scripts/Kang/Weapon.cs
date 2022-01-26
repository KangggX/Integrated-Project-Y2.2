using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrel;
    RaycastHit hit;

    private void Update()
    {
        Debug.DrawRay(_gunBarrel.position, _gunBarrel.up * 1000, Color.green);

        if (Physics.Raycast(_gunBarrel.position, _gunBarrel.up, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}
