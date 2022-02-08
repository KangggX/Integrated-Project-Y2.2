using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwingMovement : MonoBehaviour
{
    //gameobjects
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject cameraCenter;
    public GameObject forwardDirection;

    //character controller
    private CharacterController cController;

    //Vector 3
    private Vector3 positionPreviousFrameLeftHand;
    private Vector3 positionPreviousFrameRightHand;
    private Vector3 playerPositionPreviousFrame;
    private Vector3 playerPositionThisFrame;
    private Vector3 positionThisFrameLeftHand;
    private Vector3 positionThisFrameRightHand;

    //speed
    public float speed = 70;
    [SerializeField]
    private float handSpeed;

    public float maxSpeed;
    public float zeroToMax = 2.5f;
    private float accelRate;
    private float forwardVelocity;

    public float playerAcceleration;
    public float skiVelocity;
    public float dragForce;
    public float skiAcceleration = 0;

    //check if Skiing
    [SerializeField]
    private bool isSki;
    public bool touchedSnow;

    

    // Start is called before the first frame update
    void Start()
    {
        accelRate = maxSpeed / zeroToMax;
        forwardVelocity = 0;

        dragForce = 1;
        cController = GetComponent<CharacterController>();

        //set previous frame
        playerPositionPreviousFrame = transform.position;
        positionPreviousFrameLeftHand = leftHand.transform.position;
        positionPreviousFrameRightHand = rightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        SnowInteracted();
        CenterEye();
        PlayerPosition();
    }

    private void CenterEye()
    {
        float yRotation = cameraCenter.transform.eulerAngles.y;
        forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);
    }

    //Walk Motion
    private void PlayerPosition()
    {
        if(touchedSnow == false)
        {
            isSki = false;
        }

        //Hands
        positionThisFrameLeftHand = leftHand.transform.position;
        positionThisFrameRightHand = rightHand.transform.position;

        //Player
        playerPositionThisFrame = transform.position;

        //Last frame
        var playerDistanceMoved = Vector3.Distance(playerPositionThisFrame, playerPositionPreviousFrame);
        var leftHandDistance = Vector3.Distance(positionPreviousFrameLeftHand, positionThisFrameLeftHand);
        var rightHandDistance = Vector3.Distance(positionPreviousFrameRightHand, positionThisFrameRightHand);

        Vector3 velocity = cController.velocity;

        skiVelocity = playerDistanceMoved / 1f;

        //get hand speed
        handSpeed = ((leftHandDistance - playerDistanceMoved) + (rightHandDistance - playerDistanceMoved));

        //making player move
        if (Time.timeSinceLevelLoad > 1f)
        {
            //apply movement type
            if(isSki == true)
            {
                if (handSpeed>=0.001)
                {
                    velocity += forwardDirection.transform.forward * playerAcceleration/7* skiAcceleration * Time.deltaTime;
                }
                //Warning: change in framerate may cause change in speed

                if (skiAcceleration >= 2)
                {
                    skiAcceleration -= (float)(0.001);
                }

                velocity *= dragForce/2;

                cController.Move(velocity);
                touchedSnow = false;
            }
            else
            {

                forwardVelocity += accelRate * Time.deltaTime;
                forwardVelocity = Mathf.Min(forwardVelocity, maxSpeed);

            }
        }

        //setting previous position
        positionPreviousFrameLeftHand = positionThisFrameLeftHand;
        positionPreviousFrameRightHand = positionThisFrameRightHand;

        playerPositionPreviousFrame = playerPositionThisFrame;
    }

    public void SnowInteracted()
    {
        if (touchedSnow && (Time.timeSinceLevelLoad > 5f))
        {
            //isSki = true;
            skiAcceleration = 10;
        }
    }

    public void SnowRelease()
    {
        isSki = false;
    }

    private void LateUpdate()
    {
                cController.Move (transform.forward * forwardVelocity);
        
    }
}
