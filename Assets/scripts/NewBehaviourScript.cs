using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _canMoveUp = true;
    public bool _canMoveDown = true;
    public bool _canMoveLeft = true;
    public bool _canMoveRight = true;

    public float Speed = 5;
    public float Jump = 5;
    public bool CanJump = false;
    public float LimitTimeJump = 5;
    public bool Isoteric = false;

    bool Jumped = true;
    float RecordTimeJump = 0;
    bool istrig;
    private void Start()
    {
       
    }
    // Update is called once per frame

    void FixedUpdate()
    {
        if(istrig == false && RecordTimeJump >= LimitTimeJump)
        {
            Jumped = false;
        }
        if(istrig == true)
        {
            Jumped = true;
        }
        if (Input.GetKey(KeyCode.W) && _canMoveUp == true && CanJump == false && Isoteric == false)
        {
            gameObject.transform.position += new Vector3(0, Time.deltaTime * Speed, 0);
        }
        if (Input.GetKey(KeyCode.W) && _canMoveUp == true && CanJump == true && Jumped == true && Isoteric == false)
        {
            if (RecordTimeJump < LimitTimeJump)
            {
                RecordTimeJump += 0.01f;
            }
            if (RecordTimeJump >= LimitTimeJump)
            {
                Jumped = false;
                RecordTimeJump = 0;
            }
            gameObject.transform.position += new Vector3(0, Time.deltaTime * Jump, 0);
        }
        if (Input.GetKey(KeyCode.D) && _canMoveRight == true && Isoteric == false)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.A) && _canMoveLeft == true && Isoteric == false)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * -Speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.S) && _canMoveDown == true && Isoteric == false)
        {
            gameObject.transform.position += new Vector3(0, Time.deltaTime * -Speed, 0);
        }

        if (Input.GetKey(KeyCode.W) && _canMoveUp == true && CanJump == false && Isoteric == true)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * Speed, Time.deltaTime * Speed * 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.D) && _canMoveRight == true && Isoteric == true)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * Speed, Time.deltaTime * -Speed * 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.A) && _canMoveLeft == true && Isoteric == true)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * -Speed, Time.deltaTime * Speed * 0.5f, 0);
        }
        if (Input.GetKey(KeyCode.S) && _canMoveDown == true && Isoteric == true)
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * -Speed, Time.deltaTime * -Speed * 0.5f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        istrig = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        istrig = false;
    }
}
