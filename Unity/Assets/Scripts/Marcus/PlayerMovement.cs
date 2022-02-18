/*
Author: Lee Ka Meng Marcus

Name of Class: Movement

Description of Class: Movement for vr

Date Created: 10 / 02 / 2022
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

[System.Serializable]
public class PlayerMovement : MonoBehaviour
{

    //base values
    public float speed = 1;
    public float initialSpeed;
    public XRNode inputSource;
    public float gravity = -9.81f;

    //layers
    public LayerMask groundLayer;


    //controllers
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    public XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController character;
    public Camera playerCamera;


    private void Awake()
    {
        initialSpeed = speed;
    }

    //get script
    private void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }


    private void Update()
    {
        //get input
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        
    }
    private void FixedUpdate()
    {
        CapsuleFollowHeadset();


        //find direction
        Quaternion headYaw = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);  
        Vector3 direction =headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        //transform player
        character.Move(direction * Time.fixedDeltaTime * speed);
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    //find direction
    void CapsuleFollowHeadset()
    {
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(playerCamera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    //check for ground
    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
