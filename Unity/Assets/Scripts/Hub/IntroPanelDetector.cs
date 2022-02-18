using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kang Xuan

Name of Class: IntroPanelDetector

Description of Class: Handles the intro panel at the start of the game

Date Created: 18/02/2022
**/
public class IntroPanelDetector : MonoBehaviour
{
    [SerializeField] private GameObject _frontPanel;

    // Disable the current panel once triggered
    // Enable subsequent panel (if have)
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.gameObject.SetActive(false);

        if (_frontPanel != null)
        {
            _frontPanel.SetActive(true);
        }
    }

    // Gizmos to check where the interaction point is on Scene View
    private void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x + GetComponent<BoxCollider>().center.x, transform.position.y + GetComponent<BoxCollider>().center.y, transform.position.z + GetComponent<BoxCollider>().center.z);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos, GetComponent<BoxCollider>().size);
    }
}
