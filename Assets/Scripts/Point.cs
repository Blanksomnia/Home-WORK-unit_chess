using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum statusPoints
{
    empty,
    end,
    move,
    star
}
public class Point : MonoBehaviour
{
    public statusPoints status;
    [HideInInspector] public GameObject ImageStar = null;
    [HideInInspector] public GameObject ImageBackMove = null;

    [HideInInspector] public Point PUP = null;
    [HideInInspector] public Point PDOWN = null;
    [HideInInspector] public Point PLEFT = null;
    [HideInInspector] public Point PRIGHT = null;

}
