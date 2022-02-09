using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class OutShotgun : OutWeapon
{
    [SerializeField]
    private Projectile bulletPrefab;

    public TextMeshProUGUI DebugText;
    protected override void StartShooting(ActivateEventArgs interactor)
    {
        base.StartShooting(interactor);
        Shoot();
        
        
    }
    

    protected override void Shoot()
    {
        if(base.canShootAmmo == true)
        {
            base.Shoot();
            for (int i = 0; i < 4; i++) 
            {
                Projectile projectileInstantance = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                projectileInstantance.Init(this);
                projectileInstantance.Launch();
            }
            Debug.Log("Bullet has been shot");
            DebugText.text = "Shotgun : Shot";
        }

        else
        {
            DebugText.text = "Shotgun : Cooldown";
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
                DebugText.text = "Shotgun : Ready";
            }
        
    }
}
