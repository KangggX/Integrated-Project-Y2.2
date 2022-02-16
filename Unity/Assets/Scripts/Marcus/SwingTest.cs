using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwingTest : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject centerBody;

    public float angleBetween;

    private void Update()
    {
        TiltAngle();
    }

    private void TiltAngle()
    {
        angleBetween = Vector3.Angle(centerBody.transform.up, playerCamera.transform.position);
    }
}
