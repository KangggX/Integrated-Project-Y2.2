using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutTarget : MonoBehaviour
{
    private GameManager gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        
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
