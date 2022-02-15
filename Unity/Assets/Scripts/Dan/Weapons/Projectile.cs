  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
