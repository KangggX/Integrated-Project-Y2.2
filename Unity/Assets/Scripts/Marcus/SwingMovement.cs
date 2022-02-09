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

    //track hand movement
    [SerializeField]
    private float handSpeed;

    //movement value
    public float maxWalkSpeed;
    public float zeroToMaxWalk = 6f;
    public float maxToZeroWalk = 3f;
    private float accelRateWalk;
    private float deccelRateWalk;

    public float maxSkiSpeed;
    public float zeroTomaxSki = 0.5f;
    public float maxToZeroSki = 10f;
    private float accelRateSKi;
    private float deccelRateSki;

    private float forwardVelocity;

    //check movement type
    public bool touchedSnow;
    public bool skiing;

    

    // Start is called before the first frame update
    void Start()
    {
        accelRateWalk = maxWalkSpeed / zeroToMaxWalk;
        deccelRateWalk = -maxWalkSpeed / maxToZeroWalk;

        accelRateSKi = maxSkiSpeed / zeroTomaxSki;
        deccelRateSki = -maxSkiSpeed / maxToZeroSki;

        forwardVelocity = 0;

        cController = GetComponent<CharacterController>();

        //set previous frame
        playerPositionPreviousFrame = transform.position;
        positionPreviousFrameLeftHand = leftHand.transform.position;
        positionPreviousFrameRightHand = rightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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

        //Hands
        positionThisFrameLeftHand = leftHand.transform.position;
        positionThisFrameRightHand = rightHand.transform.position;

        //Player
        playerPositionThisFrame = transform.position;

        //Last frame
        var playerDistanceMoved = Vector3.Distance(playerPositionThisFrame, playerPositionPreviousFrame);
        var leftHandDistance = Vector3.Distance(positionPreviousFrameLeftHand, positionThisFrameLeftHand);
        var rightHandDistance = Vector3.Distance(positionPreviousFrameRightHand, positionThisFrameRightHand);

        Debug.Log(playerDistanceMoved);
        Debug.Log(leftHandDistance);
        Debug.Log(rightHandDistance);
        //get hand speed
        handSpeed = ((leftHandDistance - playerDistanceMoved) + (rightHandDistance - playerDistanceMoved));

        //making player move
        if (Time.timeSinceLevelLoad > 1f)
        {
            //apply movement type
            if(touchedSnow == true)
            {
                if (handSpeed >= 0.01)
                {
                    forwardVelocity += accelRateSKi * Time.deltaTime;
                    forwardVelocity = Mathf.Min(forwardVelocity, maxSkiSpeed);

                    cController.Move(forwardDirection.transform.forward * forwardVelocity);
                }
                else
                {
                    forwardVelocity += deccelRateSki * Time.deltaTime;
                    forwardVelocity = Mathf.Max(forwardVelocity, 0);

                    cController.Move(forwardDirection.transform.forward * forwardVelocity);
                }
            }
            else if (skiing)
            {
                forwardVelocity += deccelRateSki * Time.deltaTime;
                forwardVelocity = Mathf.Max(forwardVelocity, 0);

                cController.Move(forwardDirection.transform.forward * forwardVelocity);
            }
            else
            {
                if (handSpeed >= 0.001)
                {
                    forwardVelocity += accelRateWalk * Time.deltaTime;
                    forwardVelocity = Mathf.Min(forwardVelocity, maxWalkSpeed);

                    cController.Move(forwardDirection.transform.forward * forwardVelocity);
                }
                else
                {
                    forwardVelocity += deccelRateWalk * Time.deltaTime;
                    forwardVelocity = Mathf.Max(forwardVelocity, 0);

                    cController.Move(forwardDirection.transform.forward * forwardVelocity);
                }
            }
        }

        //setting previous position
        positionPreviousFrameLeftHand = positionThisFrameLeftHand;
        positionPreviousFrameRightHand = positionThisFrameRightHand;

        playerPositionPreviousFrame = playerPositionThisFrame;
    }

}
