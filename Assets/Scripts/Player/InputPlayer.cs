using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPlayer : MonoBehaviour
{
    Rigidbody rb;
    PlayerMemory player;
    AudioSource move;
    [SerializeField] AudioSource stop;
    [SerializeField] Transform Ball;
    [SerializeField] float RotateSpeed = 2f;
    [SerializeField] Transform map;

    void Awake()
    {
        move = GetComponent<AudioSource>();
        player = GetComponent<PlayerMemory>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();

        if(Input.GetKey(KeyCode.D))
        {
            map.Rotate(0, 0, RotateSpeed * Time.deltaTime);
            transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.A)) 
        {
           
            map.Rotate(0, 0, -RotateSpeed * Time.deltaTime);
            transform.Rotate(0, -RotateSpeed * Time.deltaTime, 0);

        }


    }

    private void Move()
    {
        Vector3 moving = Vector3.zero;

        if(Input.GetKeyUp(KeyCode.W))
        {
            stop.Play();
        }
        if (Input.GetKey(KeyCode.W))
        {
            moving = transform.forward * player._speed * Time.deltaTime;
            Animate();
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            stop.Play();
        }
        if (Input.GetKey(KeyCode.S))
        {
            moving = (-transform.forward * player._speed * Time.deltaTime);
            Animate();
        }

        if(moving != Vector3.zero) { rb.AddForce(moving); move.mute = false; }
        else { move.mute = true; }
    }

    void Animate()
    {
        Ball.transform.Rotate(0, 0, 21);
    }
}
