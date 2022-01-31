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

    //Vector 3
    private Vector3 positionPreviousFrameLeftHand;
    private Vector3 positionPreviousFrameRightHand;
    private Vector3 playerPositionPreviousFrame;
    private Vector3 playerPositionThisFrame;
    private Vector3 positionThisFrameLeftHand;
    private Vector3 positionThisFrameRightHand;

    //speed
    public float speed = 70;
    private float handSpeed;

    // Start is called before the first frame update
    void Start()
    {
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

        //get hand speed
        handSpeed = ((leftHandDistance - playerDistanceMoved) + (rightHandDistance - playerDistanceMoved));

        //making player move
        if (Time.timeSinceLevelLoad > 1f)
        {
            transform.position += forwardDirection.transform.forward * handSpeed * speed * Time.deltaTime;
        }

        //setting previous position
        positionPreviousFrameLeftHand = positionThisFrameLeftHand;
        positionPreviousFrameRightHand = positionThisFrameRightHand;

        playerPositionPreviousFrame = playerPositionThisFrame;
    }
}
