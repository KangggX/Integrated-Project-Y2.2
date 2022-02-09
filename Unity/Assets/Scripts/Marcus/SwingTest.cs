using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwingTest : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject cameraCenter;
    public GameObject forwardDirection;

    public TMP_Text speedIndicator;

    //character controller
    private CharacterController cController;

    //Vector 3
    private Vector3 positionPreviousFrameLeftHand;
    private Vector3 positionPreviousFrameRightHand;
    private Vector3 playerPositionPreviousFrame;
    private Vector3 playerPositionThisFrame;
    private Vector3 positionThisFrameLeftHand;
    private Vector3 positionThisFrameRightHand;

    public float maximumHandSpeed;

    //speed
    public float speed = 70;
    [SerializeField]
    private float handSpeed;

    private void Start()
    {
        playerPositionPreviousFrame = transform.position;
        positionPreviousFrameLeftHand = leftHand.transform.position;
        positionPreviousFrameRightHand = rightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = cameraCenter.transform.eulerAngles.y;
        forwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

        positionThisFrameLeftHand = leftHand.transform.position;
        positionThisFrameRightHand = rightHand.transform.position;

        //Player
        playerPositionThisFrame = transform.position;

        //Last frame
        var playerDistanceMoved = Vector3.Distance(playerPositionThisFrame, playerPositionPreviousFrame);
        var leftHandDistance = Vector3.Distance(positionPreviousFrameLeftHand, positionThisFrameLeftHand);
        var rightHandDistance = Vector3.Distance(positionPreviousFrameRightHand, positionThisFrameRightHand);

        handSpeed = ((leftHandDistance - playerDistanceMoved) + (rightHandDistance - playerDistanceMoved));

        if (handSpeed > maximumHandSpeed)
        {
            maximumHandSpeed = handSpeed;
            speedIndicator.text = maximumHandSpeed.ToString();
        }
    }
}
