using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting.ReorderableList;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;


[CustomEditor(typeof(PointsEdit))]
[CanEditMultipleObjects]
public class EditorGUIpoints : Editor
{
    public void OnSceneGUI()
    {
        Color color = Color.white;
        float size = 0.7f;
        var t = target as PointsEdit;

        GUIStyle style = new GUIStyle();

        for (int i = 0; i < t.list.points.Count; i++)
        {
            var tr = t.list.points[i].transform;
            var pos = t.list.points[i].transform.position;

            if(t.p == t.list.points[i])
            {
                color = Color.green;
            }
            else
            {
                color = Color.red;
            }

            Handles.color = color;

            if (Handles.Button(pos, Quaternion.identity, 0.7f, 0.7f, Handles.RectangleHandleCap))
            {
                t.p = t.list.points[i];

            }

        }

        GUI.color = Color.gray;

        Handles.BeginGUI();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Move", GUILayout.Width(134)))
        {
            t.AddMove();
        }
        if (GUILayout.Button("Add Star", GUILayout.Width(134)))
        {
            t.AddStar();
        }
        if (GUILayout.Button("Add Ball", GUILayout.Width(134)))
        {
            t.AddStartBall();
        }
        if (GUILayout.Button("Add End", GUILayout.Width(134)))
        {
            t.AddEnd();
        }
        GUI.color = Color.yellow;
        if (GUILayout.Button("ClearPoint", GUILayout.Width(134)))
        {
            t.ClearPoint();
        }
        GUILayout.TextArea(t.TEXT, GUILayout.Width(134));

        GUILayout.EndHorizontal();
        Handles.EndGUI(); 

    }
}

