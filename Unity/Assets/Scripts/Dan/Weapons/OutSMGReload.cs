using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Dan

Name of Class: Outweapon Reload

Description of Class: Reload mechanics for the SMG

Date Created: 3 / 02 / 2022
*/
public class OutSMGReload : MonoBehaviour
{
    public GameObject parentGun;
    private GameObject childMag;
    private OutSMG outSMG;
    // Start is called before the first frame update
    void Start()
    {
        outSMG = GameObject.Find("SMG").GetComponent<OutSMG>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "outdoorMag")
        {
            if (collision.gameObject.name == "smgMag")
            {
                collision.rigidbody.useGravity = false;
                collision.collider.enabled = false;
                collision.gameObject.transform.parent = parentGun.transform;
                collision.rigidbody.freezeRotation = true;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                
                if (collision.gameObject.transform.parent = parentGun.transform)
                {
                    collision.transform.localPosition =  new Vector3(0.0076f, 0.0236f, 0.0166f);
                }

                outSMG.addAmmo();

                Debug.LogFormat("{0}",collision.transform.localPosition);
                            

            }
        }


    }
}