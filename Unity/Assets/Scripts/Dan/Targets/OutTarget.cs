using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Dan

Name of Class: OutTarget

Description of Class: Script for when the target gets hit by a bullet

Date Created: 3 / 02 / 2022
*/
public class OutTarget : MonoBehaviour
{
    private GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        //if target gets hit by a bullet, destroy the target and add a point
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(this.gameObject);
            gameManager.OutdoorPoints += 1;
            
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
