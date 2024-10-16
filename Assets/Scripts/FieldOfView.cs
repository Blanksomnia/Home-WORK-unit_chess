using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask Wall;
    [SerializeField] private LayerMask Trash;
    [SerializeField] private LayerMask Robot;

    [SerializeField] private float _speedRot;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _radius;
    [SerializeField] private float _timePause;

    [SerializeField] private AudioSource MoveRobot;
    [SerializeField] private AudioSource Detected;
    [SerializeField] private AudioSource WallView;

    [SerializeField] private Transform LookForward;

    [HideInInspector] public bool Forward = false;
    [HideInInspector] public bool Left = false;
    [HideInInspector] public bool Right = false;

    private Quaternion _RobotRot;
    private Vector3 _trashPos;
    private bool _canSeePlayer;
    private System.Random _rand = new System.Random();

    [HideInInspector] public Vector3 TrashPos => _trashPos;
    [HideInInspector] public bool CanSeePlayer => _canSeePlayer;
    [HideInInspector] public float Radius => _radius;

    private void FindTrash()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, Trash);
        
        if (rangeChecks.Length > 0)
        {
            Transform trash = null;
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Vector3 DirectionToTrash = (rangeChecks[i].transform.position - transform.position).normalized;
                float DistanceToTrash = Vector3.Distance(transform.position, rangeChecks[i].transform.position);
                if (!Physics.Raycast(transform.position, DirectionToTrash, DistanceToTrash, Wall) && Vector3.Angle(transform.forward, DirectionToTrash) < 90)
                {
                    if (trash != null)
                    {
                        if (Vector3.Distance(transform.position, trash.position) > DistanceToTrash)
                        {
                            trash = rangeChecks[i].transform;
                        }
                    }
                    else
                    {
                        trash = rangeChecks[i].transform;
                    }
                }
                
            }
            if(trash != null)
            {
                _trashPos = trash.position;
                transform.LookAt(trash.position);
                Detected.Play();
                _canSeePlayer = true;
            }
            else
            {
                _canSeePlayer = false;
            }
        }
        else if (_canSeePlayer)
        {
            _canSeePlayer = false;
        }

    }
    private void FindWall()
    {
        if (Left == true && Right == false)
        {
            _RobotRot = Quaternion.Euler(0, 90, 0);
            OnRotate(_RobotRot);
        }
        else if (Right == true && Left == false)
        {
            _RobotRot = Quaternion.Euler(0, - 90, 0);
            OnRotate(_RobotRot);
        }
        else if (Right == true && Left == true)
        {
            _RobotRot = Quaternion.Euler(0, 180, 0);
            OnRotate(_RobotRot);
        }
        else if (Left == false && Right == false) { MoveToSeek(); }
    }
    private void MoveToSeek()
    {
        if (_canSeePlayer) { return; }
        int move = _rand.Next(0, 2);
        switch (move)
        {
            case 0:
                _RobotRot = Quaternion.Euler(0, 90, 0);
                OnRotate(_RobotRot);
                break;
            case 1:
                _RobotRot = Quaternion.Euler(0, - 90, 0);
                OnRotate(_RobotRot);
                break;
            default:break;
        }

    }


    private void Start()
    {
        MoveRobot.loop = true;
        MoveRobot.Play();
        InvokeRepeating("MoveToSeek", _timePause, _timePause);
    }
    private void ToMove(Vector3 ToMove)
    {
        transform.position = Vector3.Lerp(transform.position, ToMove, Time.deltaTime * _speedMove);
    }
    private void OnRotate(Quaternion ToRotate)
    {
        transform.rotation *= ToRotate;
    }

    private void Update()
    {
        if (Forward == true)
        {
            WallView.Play();
            FindWall();
        }


        if (!_canSeePlayer)
        {
            FindTrash();
            ToMove(LookForward.position);
        }
        else
        {
            ToMove(_trashPos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Destroy(collision.gameObject);
            transform.rotation = new Quaternion();
            _canSeePlayer = false;
        }
    }

}
