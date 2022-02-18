  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Author: Dan

Name of Class: Projectile

Description of Class: Base Projectile

Date Created: 3 / 02 / 2022
*/
public class Projectile : MonoBehaviour
{
    protected OutWeapon outweapon;

    public virtual void Init(OutWeapon outweapon)
    {
        this.outweapon = outweapon;

    }
    public virtual void Launch()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
