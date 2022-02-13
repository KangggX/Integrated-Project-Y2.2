using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanelDetector : MonoBehaviour
{
    [SerializeField] private GameObject _frontPanel;

    private void OnTriggerEnter(Collider other)
    {
        transform.parent.gameObject.SetActive(false);

        if (_frontPanel != null)
        {
            _frontPanel.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = new Vector3(transform.position.x + GetComponent<BoxCollider>().center.x, transform.position.y + GetComponent<BoxCollider>().center.y, transform.position.z + GetComponent<BoxCollider>().center.z);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(pos, GetComponent<BoxCollider>().size);
    }
}
