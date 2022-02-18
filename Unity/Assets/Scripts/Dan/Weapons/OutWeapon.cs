using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(XRGrabInteractable))]

/*
Author: Dan

Name of Class: Outweapon

Description of Class: Base Gun Mechanics

Date Created: 3 / 02 / 2022
*/

public class OutWeapon : MonoBehaviour
{
    [SerializeField] protected float shootingCooldown = 3.0f;
    protected float StartTime;

    [SerializeField]
    protected bool canShootAmmo;
    [SerializeField] protected float shootingForce;

    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] private float recoilForce;
    [SerializeField] private float damage;

    private Rigidbody rigidBody;
    private XRGrabInteractable InteractableWeapon;

    protected virtual void Awake()
    {
        InteractableWeapon = GetComponent<XRGrabInteractable>();
        rigidBody = GetComponent<Rigidbody>();

        SetupInteractableWeaponEvents();

    }

    private void SetupInteractableWeaponEvents()
    {
        //Event Listeners
        InteractableWeapon.selectEntered.AddListener(PickUpWeapon);
        InteractableWeapon.selectExited.AddListener(DropWeapon);
        InteractableWeapon.activated.AddListener(StartShooting);
        InteractableWeapon.deactivated.AddListener(StopShooting);
        
    }

    private void PickUpWeapon(SelectEnterEventArgs interactor)
    {
        //GetComponent<MeshHider>().Hide();
        Debug.Log("Mesh is hidden");
    }
    private void DropWeapon(SelectExitEventArgs interactor)
    {
       //GetComponent<MeshHider>().Show();
        Debug.Log("Mesh is shown");
    }
    protected virtual void StartShooting(ActivateEventArgs interactor)
    {   
    }
    protected virtual void StopShooting(DeactivateEventArgs interactor)
    {   
    }

    protected virtual void Shoot()
    {

        ApplyRecoil();
        //resets gun cooldown
        canShootAmmo = false;
        StartTime = Time.time;
    }

    private void ApplyRecoil()
    {
        // adds a force backwords on the gun
        rigidBody.AddRelativeForce(Vector3.back * recoilForce, ForceMode.Impulse);
    }

    public float GetShootingForce()
    {
        return shootingForce;
    }


    // Start is called before the first frame update
    void Start()
    {
        StartTime = Time.time;
        canShootAmmo = true;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
