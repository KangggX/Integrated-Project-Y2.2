using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetTransform
{
    [MenuItem("Modify/Reset Transform #r", true)]
    public static bool SelectionValidate()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("Modify/Reset Transform #r")]
    public static void ResetObjectTransform()
    {
        foreach (GameObject t in Selection.gameObjects)
        {
            if (t != null)
            {
                Undo.RecordObject(t.transform, "Undo Reset Transform");
                t.transform.position = Vector3.zero;
            }
        }
    }
}
