using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkiing : MonoBehaviour
{
    public GameObject player;
    public void Grab()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }
    public void Release()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false;

    }
    public void SkiSwing()
    {
            player.GetComponent<SwingMovement>().touchedSnow = true;
    }
    public void SkiStop()
    {
            player.GetComponent<SwingMovement>().touchedSnow = false;
    }
}
