using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModificationTools
{
    /// <summary>
    /// % = ctrl
    /// & = alt
    /// # = shift
    /// </summary>

    /// <summary>
    /// Resets the transform of selected objects to (0, 0, 0) in World Space
    /// </summary>
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

    /// <summary>
    /// Snaps position of selected objects to nearest integer.
    /// </summary>
    [MenuItem("Modify/Snap #s", isValidateFunction: true)]
    public static bool SnapValidate()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("Modify/Snap #s")]
    public static void Snap()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Vector3 op = go.transform.position; // op, short for objectPosition
            Undo.RecordObject(go.transform, "Object Snap");
            go.transform.position = new Vector3(Mathf.Round(op.x), Mathf.Round(op.y), Mathf.Round(op.z));
        }
    }
}
