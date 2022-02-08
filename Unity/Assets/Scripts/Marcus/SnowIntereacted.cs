using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowIntereacted : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider snow)
    {
        print("hi");
        player.GetComponent<SwingMovement>().touchedSnow = true;
    }
    private void OnTriggerExit(Collider snow)
    {
        print("hi");
        player.GetComponent<SwingMovement>().touchedSnow = false;
    }

}
