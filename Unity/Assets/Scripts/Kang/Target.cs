using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int _currPoint = 0;

    public void GainPoint()
    {
        _currPoint++;
        Debug.Log(_currPoint);
    }

    public void ClearBulletHoleInstances()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform hole in transform)
            {
                Destroy(hole.gameObject);
            }
        }
    }
}
