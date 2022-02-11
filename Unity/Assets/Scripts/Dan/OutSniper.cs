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

    public TextMeshProUGUI DebugText;
    public GameObject magazine;
    public int currentAmmo = 5;
    protected override void StartShooting(ActivateEventArgs interactor)
    {
        if (currentAmmo > 0)
        {
            base.StartShooting(interactor);
            Shoot();
        }
        else
        {
            //unloads the gun magazine
            magazine.transform.parent = null;
            magazine.AddComponent<Rigidbody>();
            
            DebugText.text = "Sniper : Magazine Not Loaded";
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
            DebugText.text = "Sniper : Shot";

            currentAmmo -= 1;


            
            
        }

        else
        {
            DebugText.text = "Sniper : Cooldown";
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
                Debug.Log("Finished Cooldown");
                DebugText.text = "Sniper : Ready";
            }
    }
}
