using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public bool isgrounded = true;
    private void OnCollisionEnter(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {
            isgrounded = true;
        }
    }
    private void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {
            isgrounded = false;
        }
    }
}
