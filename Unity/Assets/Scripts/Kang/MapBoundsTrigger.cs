using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kirdesh

Name of Class: MapBoundsTrigger

Description of Class: This class will ensure players don't go out of the map

Date Created: 18/02/2022
**/
public class MapBoundsTrigger : MonoBehaviour
{
    [SerializeField] private Transform bouncedBackPosition;

    // Trigger to bring player back to the surface
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = bouncedBackPosition.position;
        }
    }
}
