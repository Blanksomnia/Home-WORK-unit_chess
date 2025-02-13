using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class PointsEdit : MonoBehaviour
{
    public Point p;
     public string TEXT = "Info";

    [SerializeField] public ListPoints list;

    WaitForSeconds wait = new WaitForSeconds(3);
    [SerializeField] Transform ParentNonMovement;
    [SerializeField] Transform ParentMovement;
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject End;
    [SerializeField] GameObject Star;
    [SerializeField] GameObject Move;


    public void AddStar()
    {
        if(p.status == statusPoints.move)
        {
            GameObject star = Instantiate(Star, ParentMovement, false);
            p.ImageStar = star;
            star.transform.position = p.transform.position;
            p.status = statusPoints.star;
            list.stars.Add(star.transform);
        }
        else
        {
            TEXT = "Poin have be a <Move>";
        }
    }

    public void AddMove()
    {
        if(p.status == statusPoints.empty)
        {
            GameObject move = Instantiate(Move, ParentNonMovement, false);
            move.transform.position = p.transform.position;
            p.ImageBackMove = move;
            p.status = statusPoints.move;
        }
        else
        {
            TEXT = "Point have be <Empty>";

        }

    }

    public void AddStartBall()
    {
        if(p.status == statusPoints.move)
        {
            if(list.YOU == null)
            {
                list.YOU = p;
                list.Ball = Instantiate(Ball, ParentMovement).transform;
                list.Ball.transform.position = p.transform.position;
            }
            else
            {
                TEXT = "Ball have be alone";

            }
        }
        else
        {
            TEXT = "Poin have be a <Move>";

        }

    }

    public void AddEnd()
    {
        if (p.status == statusPoints.move)
        {
            if(list.end == null)
            {
                list.end = Instantiate(End, ParentMovement, false).transform;
                list.end.position = p.transform.position;
                p.status = statusPoints.end;
            }
            else
            {
                TEXT = "End have be alone";

            }
        }
        else
        {
            TEXT = "Poin have be a <Move>";

        }

    }

    public void ClearPoint()
    {
        if(list.YOU == p)
        {
            ClearMove(p); list.YOU = null; list.Ball.gameObject.SetActive(false); DestroyImmediate(list.Ball.gameObject); list.Ball = null;
        }
        else
        {
            switch (p.status)
            {
                case statusPoints.empty: TEXT = "Point is already <Empty>";  break;
                case statusPoints.move: { ClearMove(p); } break;
                case statusPoints.star: {
                        ClearMove(p);
                        for (int i = 0; i < list.stars.Count; i++)
                        {
                            if(p.ImageStar.transform == list.stars[i])
                            {
                                list.stars.RemoveAt(i);
                            }
                        }

                        p.ImageStar.SetActive(false);
                        DestroyImmediate(p.ImageStar);
                        p.ImageStar = null; 
                    } break;
                case statusPoints.end: { ClearMove(p); list.end.gameObject.SetActive(false); DestroyImmediate(list.end.gameObject); list.end = null; } break;
            }
        }

    }

    private void ClearMove(Point p)
    {
        p.status = statusPoints.empty; 
        p.ImageBackMove.SetActive(false);
        DestroyImmediate(p.ImageBackMove);
        p.ImageBackMove = null;
    }
}
