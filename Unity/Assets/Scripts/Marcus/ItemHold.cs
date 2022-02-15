using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHold : MonoBehaviour
{
    public void Grab()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = true;
    }
    public void Release()
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false;
    }
}
