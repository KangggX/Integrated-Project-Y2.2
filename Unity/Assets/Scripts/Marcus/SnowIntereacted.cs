using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowIntereacted : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerStay(Collider snow)
    {
        print("hi");
        player.GetComponent<SwingMovement>().touchedSnow = true;
    }
}
