using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsProjectile : Projectile
{
    [SerializeField] private float lifeTime;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void Init(OutWeapon outweapon)
    {
        base.Init(outweapon);
        //destroys the bullet after a certain amount of time has passed
        Destroy(gameObject, lifeTime);
    }
    
    public override void Launch()
    {
        base.Launch();
        //adds a forward force to push the bullet
        rigidBody.AddRelativeForce(Vector3.forward * outweapon.GetShootingForce(), ForceMode.Impulse);
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
