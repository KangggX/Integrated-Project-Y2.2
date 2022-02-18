/*
Author: Lee Ka Meng Marcus

Name of Class: Animation

Description of Class: Plays animation for guides

Date Created: 10 / 02 / 2022
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiAnimation : MonoBehaviour
{
    //store action done
    public bool firstAction;
    public bool secondAction;
    public bool thirdAction;

    //find video
    public GameObject firstvid;
    public GameObject secondvid;
    public GameObject thirdvid;


    private void Start()
    {
        firstAction = false;
        secondAction = false;
        thirdAction = false;
    }
    private void Update()
    {
        //play animation when button pressed
        if (firstAction == true)
        {
            firstvid.SetActive(false);
            secondvid.SetActive(true);
        }
        if (secondAction == true)
        {
            secondvid.SetActive(false);
            thirdvid.SetActive(true);
        }
        if(thirdAction== true)
        {
            thirdvid.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    //trigger for actions
    public void ActivateFirstAction()
    {
        firstAction = true;
    }
    public void ActivateSecondAction()
    {
        secondAction = true;
    }
    public void ActivateThirdAction()
    {
        thirdAction = true;
    }
}
