using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkiing : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider player)
    {
        print("skiing");
        player.GetComponent<SwingMovement>().skiing = true;
    }
    private void OnTriggerExit(Collider player)
    {
        print("not skiing");
        player.GetComponent<SwingMovement>().skiing = false;

    }
}
