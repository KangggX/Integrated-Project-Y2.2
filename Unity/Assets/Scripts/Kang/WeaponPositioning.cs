using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WeaponPositioning : MonoBehaviour
{
    [Header("Weapon Positioning in Root")]
    public Vector3 _position;

    public virtual void Update()
    {
        if (transform.parent != null)
        {
            gameObject.transform.localPosition = _position;
        }
    }
}
