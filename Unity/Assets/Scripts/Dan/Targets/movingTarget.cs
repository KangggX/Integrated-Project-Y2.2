using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingTarget : MonoBehaviour
{
    public float moveSpeed = 10;
    [SerializeField]
    private bool movingLeft;

    public float minPos;
    public float maxPos;
    void Start() 
    {
        //gets the target moving to the left when it has been activated
        movingLeft = true;
        
       
    }
    void Update() 
    {
        
        if (movingLeft == true) 
        {
            // move left
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            //checks if it moves out of its range, then switches direction
            if (transform.position.x <= minPos) 
            {
                movingLeft = false;
            }
        } 
        else 
        {
            // move right
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            //checks if it moves out of its range, then switches direction            
            if (transform.position.x >= maxPos) 
            {
                movingLeft = true;
            }
        }
    }
}
