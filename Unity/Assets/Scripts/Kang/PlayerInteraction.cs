using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _raycastLength;
    private GameObject _mainCamera;
    private RaycastHit hit;

    private void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        InteractionRaycast();
    }

    private void InteractionRaycast()
    {
        Debug.DrawRay(_mainCamera.transform.position, _mainCamera.transform.forward * _raycastLength, Color.green);

        if (Physics.Raycast(_mainCamera.transform.position, _mainCamera.transform.forward * _raycastLength, out hit))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                
            if (interactable != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.TriggerInteraction();
                }
            }
        }
    }
}
