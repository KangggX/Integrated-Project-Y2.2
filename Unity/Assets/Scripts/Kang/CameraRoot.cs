using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRoot : MonoBehaviour
{
    [SerializeField] private Transform _cameraRoot;
    public Camera playerCamera;

    private void Update()
    {
        if (_cameraRoot != null && playerCamera != null)
        {
            playerCamera.transform.position = _cameraRoot.position;
        }
    }
}
