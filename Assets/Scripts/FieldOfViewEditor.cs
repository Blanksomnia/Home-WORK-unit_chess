using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);
        Handles.color = Color.red;
        if (fov.CanSeePlayer == false)
        {
        }
        if (fov.CanSeePlayer == true)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.TrashPos);
        }
    }
}