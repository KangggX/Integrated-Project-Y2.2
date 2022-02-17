using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiAnimation : MonoBehaviour
{
    public bool firstAction;
    public bool secondAction;
    public bool thirdAction;

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
