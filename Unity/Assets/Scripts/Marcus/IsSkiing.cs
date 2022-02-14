using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkiing : MonoBehaviour
{
    public GameObject thisObject;
    public GameObject player;

    private void Start()
    {
        thisObject = this.gameObject;
    }

    public void InSki()
    {
        player.GetComponent<SwingMovement>().skiing = true;
        thisObject.GetComponent<Collider>().isTrigger = true;
    }
    public void InSkiNot()
    {
        player.GetComponent<SwingMovement>().skiing = false;
        thisObject.GetComponent<Collider>().isTrigger = false;
    }
    public void SkiSwing()
    {
        if(player.GetComponent<IsGrounded>().isgrounded)
        {
            print("player is grounded");
            player.GetComponent<SwingMovement>().touchedSnow = true;
        }
    }
    public void SkiSwingNot()
    {
        if(player.GetComponent<IsGrounded>().isgrounded == false)
        {
            print("not grouneded");
        }
        player.GetComponent<SwingMovement>().touchedSnow = false;
    }
}
