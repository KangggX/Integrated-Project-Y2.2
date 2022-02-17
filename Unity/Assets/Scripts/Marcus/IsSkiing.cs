using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkiing : MonoBehaviour
{
    public GameObject player;
    public void SkiSwing()
    {
            player.GetComponent<SwingMovement>().touchedSnow = true;
    }
    public void SkiStop()
    {
            player.GetComponent<SwingMovement>().touchedSnow = false;
    }
}
