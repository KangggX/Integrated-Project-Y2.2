using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Controller Settings")]
    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] private float _rotationSensitivity = 40;
    private Camera _camera;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Start()
    {
        _camera = GetComponent<CameraRoot>().playerCamera;
    }

    void Update()
    {
        PlayerRotation();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector3 movementInput = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

        rb.MovePosition(transform.position + movementInput * _movementSpeed * Time.deltaTime);
    }

    private void PlayerRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotationSensitivity * Time.deltaTime;

        Vector3 playerRotation = transform.rotation.eulerAngles;
        playerRotation.y += mouseX;

        transform.rotation = Quaternion.Euler(playerRotation);
        _camera.transform.rotation = Quaternion.Euler(playerRotation);
    }

    private void CameraRotation()
    {

    }
}
