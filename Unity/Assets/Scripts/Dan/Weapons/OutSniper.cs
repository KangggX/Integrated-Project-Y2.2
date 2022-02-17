using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class OutSniper : OutWeapon
{
    [SerializeField]
    private Projectile bulletPrefab;

    //public TextMeshProUGUI DebugText;
    public GameObject magazine;
    public GameObject magazineHolder;
    public int currentAmmo = 5;

     private float reloadMagTime;
    protected override void StartShooting(ActivateEventArgs interactor)
    {
        // if the gun still has ammo
        if (currentAmmo > 0)
        {
            base.StartShooting(interactor);
            Shoot();
        }
        // no more ammo
        else
        {
            //unloads the gun magazine
            magazine.transform.parent = null;
            magazine.AddComponent<Rigidbody>();

            magazineHolder.SetActive(true);

            
            

            //DebugText.text = "Sniper : Magazine Not Loaded";
        }
        
    }
    

    protected override void Shoot()
    {
        // has ammo and can shoot
        if(base.canShootAmmo == true)
        {
            base.Shoot();
            // creates a projectile at the bullet spawn
            Projectile projectileInstantance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            projectileInstantance.Init(this);
            projectileInstantance.Launch();
            Debug.Log("Bullet has been shot");
            //DebugText.text = "Sniper : Shot";

            currentAmmo -= 1;


            
            
        }

        else
        {
            //DebugText.text = "Sniper : Cooldown";
        }
        
    }
    // adding ammo via magazines
    public void addAmmo()
    {
        currentAmmo += 5;
    }

        
    

    protected override void StopShooting(DeactivateEventArgs interactor)
    {
        base.StopShooting(interactor);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // gun cooldown checks if enough time has passed between shots fired
        if (Time.time - base.StartTime > base.shootingCooldown)
            {
                base.canShootAmmo = true;
                //Debug.Log("Finished Cooldown");
                //DebugText.text = "Sniper : Ready";
            }
    }
}
