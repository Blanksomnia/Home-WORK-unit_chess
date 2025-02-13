using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LogicPoints : MonoBehaviour
{
    public ListPoints list;

    [SerializeField] GameObject complete;

    public bool canMove = false;
    public bool canRotate = false;
    public bool Activated = true;
    public bool End = false;

    Vector3 posend;
    int Stars = 0;

    public void MoveStatus(Point stEnd)
    {
        if (stEnd != null && list.YOU.status != statusPoints.empty)
        {
            switch (stEnd.status)
            {

                case statusPoints.move:
                    {

                        posend = stEnd.transform.position;
                        canMove = true;
                        canRotate = false;
                        Activated = false;
                        list.YOU = stEnd;
                    }
                    break;

                case statusPoints.end: {
                        posend = stEnd.transform.position;
                        canMove = true;
                        canRotate = false;
                        Activated = false; End = true; CompleteLevel(); } break;

                case statusPoints.star:
                    {
                        posend = stEnd.transform.position;
                        canMove = true;
                        canRotate = false;
                        Activated = false;
                        GetStar(stEnd.ImageStar);
                        stEnd.status = statusPoints.move;
                        list.YOU = stEnd;
                    }
                    break;

                default: break;
            }

        }
    }

    public void UpdatePoints()
    {
        for (int i = 0; i < list.points.Count; i++)
        {
            Point pt = list.points[i];
            if(pt.status == statusPoints.star)
            {
                pt.ImageStar = null;
            }

            pt.status = statusPoints.empty;

            if(pt.ImageBackMove != null)
            {
                pt.status = statusPoints.move;
            }

            for (int j = 0; j < list.stars.Count; j++)
            {
                    if (pt.transform.position == list.stars[j].position && pt.status == statusPoints.move)
                    {
                        pt.ImageStar = list.stars[j].gameObject;
                        pt.status = statusPoints.star;
                    }

            }

            if (pt.transform.position == list.end.position && pt.status == statusPoints.move)
            {
                pt.status = statusPoints.end;
            }

            if (pt.transform.position == list.Ball.position)
            {
                list.YOU = pt;
            }
        }
    }
    private void GetStar(GameObject star)
    {
        for (int i = 0; i < list.stars.Count; i++)
        {
            if (star.transform == list.stars[i])
            {
                list.stars.RemoveAt(i);
            }
        }
        Stars++;
        print("StarsCount: " + Stars);
        star.SetActive(false);
        Destroy(star);
    }

    void CompleteLevel()
    {
        complete.SetActive(true);
    }

    private void Update()
    {
        if (canMove && !Activated)
        {
            AnimateMove();
        }
    }

    void AnimateMove()
    {
        Vector3 p = list.Ball.position;
        list.Ball.position = Vector3.Lerp(list.Ball.position, posend, 11.0f * Time.deltaTime);

        if(p == list.Ball.position && !End)
        {
            list.Ball.position = posend;
            posend = new Vector3();
            canRotate = false;
            canMove = false;
            Activated = true;
        }
        else if(p == list.Ball.position && End)
        {
            canRotate = false;
            canMove = false;
            Activated = false;
        }

    }

}
