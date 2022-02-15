using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSki : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = this.gameObject;
    }

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
