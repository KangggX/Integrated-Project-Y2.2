/*
Author: Lee Ka Meng Marcus

Name of Class: Identifier

Description of Class: To identify movement type

Date Created: 10 / 02 / 2022
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSki : MonoBehaviour
{
    //player
    public GameObject player;

    private void Start()
    {
        player = this.gameObject;
    }

    //trigger for movement types
    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "snow")
        {
            player.GetComponent<SwingMovement>().skiing = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "snow")
        {
            player.GetComponent<SwingMovement>().skiing = false;
        }
    }
}
