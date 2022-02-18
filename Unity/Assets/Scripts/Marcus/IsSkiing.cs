/*
Author: Lee Ka Meng Marcus

Name of Class: Identifier

Description of Class: to identify skiing varaible

Date Created: 10 / 02 / 2022
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSkiing : MonoBehaviour
{
    //player
    public GameObject player;

    //ski trigger
    public void SkiSwing()
    {
            player.GetComponent<SwingMovement>().touchedSnow = true;
    }
    public void SkiStop()
    {
            player.GetComponent<SwingMovement>().touchedSnow = false;
    }
}
