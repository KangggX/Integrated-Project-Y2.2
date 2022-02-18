using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

/*
Author: Dan

Name of Class: Outweapon

Description of Class: Gun mechanics for the RPG

Date Created: 3 / 02 / 2022
*/
public class OutRPG : OutWeapon
{

    // refer to OutSniper script
    [SerializeField]
    private Projectile bulletPrefab;

    //public TextMeshProUGUI DebugText;
    public GameObject magazine;
    public GameObject magazineHolder;
    public int currentAmmo = 1;
    public bool gunTrigger;
    
    public GameObject rocket;

    
    protected override void StartShooting(ActivateEventArgs interactor)
    {
        
        if (currentAmmo > 0)
        {
                Debug.Log("Shooting");
                base.StartShooting(interactor);
                Shoot();
        }
        else
        {
            
        }
        
            
        
    }
    

    protected override void Shoot()
    {
        if(base.canShootAmmo == true)
        {
            
            
                base.Shoot();
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
       
        if (Time.time - base.StartTime > base.shootingCooldown)
            {
                base.canShootAmmo = true;
                //Debug.Log("Finished Cooldown");
                //DebugText.text = "Sniper : Ready";
            }

        


            
    }

}
