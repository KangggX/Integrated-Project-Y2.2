using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomTools
{
    /// <summary>
    /// % = ctrl
    /// & = alt
    /// # = shift
    /// </summary>

    /// <summary>
    /// Resets the transform of selected objects to (0, 0, 0) in World Space
    /// </summary>
    [MenuItem("Custom Tools/Reset Transform #r", true)]
    public static bool SelectionValidate()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("Custom Tools/Reset Transform #r")]
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

    public static void ResetObjectTransform(GameObject go)
    {
        Undo.RecordObject(go.transform, "Undo Reset Transform");
        go.transform.position = Vector3.zero;
    }

    /// <summary>
    /// Snaps position of selected objects to nearest integer.
    /// </summary>
    [MenuItem("Custom Tools/Snap #s", isValidateFunction: true)]
    public static bool SnapValidate()
    {
        return Selection.gameObjects.Length > 0;
    }

    [MenuItem("Custom Tools/Snap #s")]
    public static void Snap()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Vector3 op = go.transform.position; // op, short for objectPosition
            Undo.RecordObject(go.transform, "Object Snap");
            go.transform.position = new Vector3(Mathf.Round(op.x), Mathf.Round(op.y), Mathf.Round(op.z));
        }
    }

    /// <summary>
    ///  Creates a new menu item 'Examples > Create Prefab' in the main menu.
    /// </summary>
    [MenuItem("Custom Tools/Convert to Prefab %#e", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
    }

    [MenuItem("Custom Tools/Convert to Prefab %#e")]
    static void CreatePrefab()
    {
        // Keep track of the currently selected GameObject(s)
        GameObject[] objectArray = Selection.gameObjects;

        // Loop through every GameObject in the array above
        foreach (GameObject gameObject in objectArray)
        {
            ResetObjectTransform(gameObject);
            PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);

            // Set the path as within the Assets folder,
            // and name it as the GameObject's name with the .Prefab format
            string localPath = "Assets/Prefabs/" + gameObject.name + ".prefab";

            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            // Create the new Prefab.
            PrefabUtility.SaveAsPrefabAsset(gameObject, localPath);
            Object.DestroyImmediate(gameObject);
        }
    }
}
