using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
Author: Kirdesh

Name of Class: ItemHold

Description of Class: Class that disables/enables a collider (technically it sets it to a trigger)
                      Mainly for items that are picked up with Grab Interactable

Date Created: 18/02/2022
**/
public class ItemHold : MonoBehaviour
{
    public void Grab()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }
    public void Release()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false;
    }
}
