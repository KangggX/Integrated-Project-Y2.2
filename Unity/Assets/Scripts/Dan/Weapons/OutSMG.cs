using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

// refer to OutSniper script
public class OutSMG : OutWeapon
{
    [SerializeField]
    private Projectile bulletPrefab;

    //public TextMeshProUGUI DebugText;
    public GameObject magazine;
    public GameObject magazineHolder;
    public int currentAmmo = 25;
    public bool gunTrigger;
    private int i;

     private float reloadMagTime;
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
            //unloads the gun magazine
            magazine.transform.parent = null;
            magazine.AddComponent<Rigidbody>();

            magazineHolder.SetActive(true);

            
            

            //DebugText.text = "Sniper : Magazine Not Loaded";
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

    public void addAmmo()
    {
        currentAmmo += 25;
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
