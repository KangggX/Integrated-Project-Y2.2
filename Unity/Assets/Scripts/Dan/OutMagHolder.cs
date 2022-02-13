using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMagHolder : MonoBehaviour
{
    public GameObject parentGun;
    private GameObject childMag;
    // Start is called before the first frame update
    void Start()
    {
        
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
