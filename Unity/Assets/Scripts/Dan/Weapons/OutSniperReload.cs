using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSniperReload : MonoBehaviour
{
    public GameObject parentGun;
    private GameObject childMag;
    private OutSniper outSniperGun;
    // Start is called before the first frame update
    void Start()
    {
        outSniperGun = GameObject.Find("Sniper").GetComponent<OutSniper>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "outdoorMag")
        {
            collision.rigidbody.useGravity = false;
            collision.collider.enabled = false;
            collision.gameObject.transform.parent = parentGun.transform;
            collision.rigidbody.freezeRotation = true;
            collision.rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
            
            if (collision.gameObject.transform.parent = parentGun.transform)
            {
                collision.transform.localPosition =  new Vector3(0f, -0.06f, -0.24f);
            }

            outSniperGun.addAmmo();

            Debug.LogFormat("{0}",collision.transform.localPosition);
                        
        }


    }
}
