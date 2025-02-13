using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ListPoints : MonoBehaviour
{
    [HideInInspector] public List<Transform> stars = new List<Transform>();
    [HideInInspector] public Transform end;
    [HideInInspector] public Point YOU;
    [HideInInspector] public Transform Ball;
    public List<Point> points = new List<Point>();

    private void Awake()
    {
        for (int i = 0; i < points.Count; i++)
        {

            Vector3 pos = points[i].transform.position;
            Vector3 posUP = pos;
            posUP.y += 1.5F;
            Vector3 posDOWN = pos;
            posDOWN.y -= 1.5F;
            Vector3 posLEFT = pos;
            posLEFT.x -= 1.5F;
            Vector3 posRIGHT = pos;
            posRIGHT.x += 1.5F;


            for (int j = 0; j < points.Count; j++)
            {
                pos = points[j].transform.position;

                if (pos == posUP)
                {
                    points[i].PUP = points[j];
                }
                else if (pos == posDOWN)
                {
                    points[i].PDOWN = points[j];
                }
                else if (pos == posLEFT)
                {
                    points[i].PLEFT = points[j];
                }
                else if (pos == posRIGHT)
                {
                    points[i].PRIGHT = points[j];
                }
            }
        }
    }
}
