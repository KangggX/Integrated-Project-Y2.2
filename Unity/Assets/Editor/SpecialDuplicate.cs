using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpecialDuplicate : EditorWindow
{
    public GameObject parent;

    private Vector3 _position;
    private Vector3 _rotation;
    private Vector3 _scale = new Vector3(1, 1, 1);
    private int _instances;

    [MenuItem("Tools/Special Duplicate %#D")]
    static void Init()
    {
        SpecialDuplicate window = (SpecialDuplicate)GetWindow(typeof(SpecialDuplicate));
        window.Show();
    }

    private void OnEnable()
    {
        Selection.selectionChanged += Repaint;
    }

    private void OnDisable()
    {
        Selection.selectionChanged -= Repaint;
    }

    private void OnGUI()
    {
        parent = (GameObject) EditorGUILayout.ObjectField("Parent", parent, typeof(GameObject), true);
        
        EditorGUILayout.Space();

        _position = EditorGUILayout.Vector3Field("Position", _position);
        _rotation = EditorGUILayout.Vector3Field("Rotation (Degrees)", _rotation);
        _scale = EditorGUILayout.Vector3Field("Scale", _scale);
        _instances = EditorGUILayout.IntField("Number of Instance(s)", _instances);

        EditorGUILayout.Space();

        using (new EditorGUI.DisabledScope(!((Selection.gameObjects.Length == 1) && (parent != null) && (_instances > 0))))
        {
            if (GUILayout.Button("Special Duplicate"))
            {
                DuplicateObject();
            }
        }
    }

    private void DuplicateObject()
    {
        for (int i = 0; i < _instances; i++)
        {
            Unsupported.DuplicateGameObjectsUsingPasteboard();

            float positionX = _position.x + Selection.activeGameObject.transform.localPosition.x;
            float positionY = _position.y + Selection.activeGameObject.transform.localPosition.y;
            float positionZ = _position.z + Selection.activeGameObject.transform.localPosition.z;

            float rotationX = _rotation.x + Selection.activeGameObject.transform.localRotation.eulerAngles.x;
            float rotationY = _rotation.y + Selection.activeGameObject.transform.localRotation.eulerAngles.y;
            float rotationZ = _rotation.z + Selection.activeGameObject.transform.localRotation.eulerAngles.z;

            float scaleX = _scale.x;
            float scaleY = _scale.y;
            float scaleZ = _scale.z;
            Selection.activeGameObject.transform.localPosition = new Vector3(positionX, positionY, positionZ);
            Selection.activeGameObject.transform.localRotation = Quaternion.Euler(new Vector3(rotationX, rotationY, rotationZ));
            Selection.activeGameObject.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
    }
}
