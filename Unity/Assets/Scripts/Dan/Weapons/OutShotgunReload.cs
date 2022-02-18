using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Dan

Name of Class: Outweapon Reload

Description of Class: Reload mechanics for the shotgun

Date Created: 3 / 02 / 2022
*/
public class OutShotgunReload : MonoBehaviour
{
    public GameObject parentGun;
    private GameObject childMag;
    private OutShotgun outShotGun;
    // Start is called before the first frame update
    void Start()
    {
        outShotGun = GameObject.Find("Shotgun").GetComponent<OutShotgun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "outdoorMag")
        {
            if (collision.gameObject.name == "shotgunMag")
            {
                collision.rigidbody.useGravity = false;
                collision.collider.enabled = false;
                collision.gameObject.transform.parent = parentGun.transform;
                collision.rigidbody.freezeRotation = true;
                collision.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
                
                if (collision.gameObject.transform.parent = parentGun.transform)
                {
                    collision.transform.localPosition =  new Vector3(0.02f, 0.09f, -0.05f);
                }

                outShotGun.addAmmo();

                Debug.LogFormat("{0}",collision.transform.localPosition);
                            

            }
        }


    }
}
