using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBarrel : MonoBehaviour
{
    RaycastHit hit;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.up * 1000, Color.green);

        if (Physics.Raycast(transform.position, transform.up, out hit))
        {
            Debug.Log(hit.transform.gameObject.name);
        }
    }
}
