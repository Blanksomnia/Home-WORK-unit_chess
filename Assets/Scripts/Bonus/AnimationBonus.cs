using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBonus : MonoBehaviour
{
    [SerializeField] float MoveDistance = 1.0f;
    [SerializeField] float SpeedMove = 1.0f;
    [SerializeField] float SpeedRotate = 1.0f;
    float MoveStart;
    float MoveEnd;
    bool isBegin = true;

    void Start()
    {
        MoveStart = transform.position.y;
        MoveEnd = transform.position.y + MoveDistance;
    }

    
    void Update()
    {
        Move();
        transform.Rotate(0, SpeedRotate * Time.deltaTime, 0);
    }

    private void Move()
    {
        if (transform.position.y >= MoveEnd)
        {
            isBegin = false;
        }
        else if (transform.position.y <= MoveStart)
        {
            isBegin = true;
        }

        float realSpeed = SpeedMove * Time.deltaTime;

        if (isBegin)
        {
            transform.position = new Vector3(transform.position.x, realSpeed + transform.position.y, transform.position.z); 
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - realSpeed, transform.position.z);
        }
        
       
    }
}
