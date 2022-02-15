using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSnipMag : MonoBehaviour
{
    public GameObject parentGun;
    private GameObject childMag;
    //private OutSniper outSniper;
    // Start is called before the first frame update
    void Start()
    {
        //outSniper = GameObject.Find("Sniper").GetComponent<OutSniper >();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "outdoorMag")
        {
            collision.gameObject.transform.parent = parentGun.transform;
            
        }


    }
}
