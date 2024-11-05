using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public enum StatePriority
{
    FindHealth = 0,
    FindPlayer = 1,
    FindGun = 2
}

public class EnemyAi : MonoBehaviour
{
    [SerializeField] LayerMask _maskLayer;
    [SerializeField] LayerMask _obstical;
    [SerializeField] float _radius;
    [SerializeField] float _radStopWithPlayer;
    [SerializeField] Transform _toMove;
    [SerializeField] Transform _left;
    [SerializeField] Transform _right;
    [SerializeField] Transform _targetPlayer;
    [SerializeField] float _timeToRotate = 5;
    [SerializeField] float _limitTostop;

    [SerializeField] float _angleLimit = 180;

    private WaitForSeconds _wait;
    private InteractWithGun _iWG;
    public Animator _animator;
    private NavMeshAgent _navMeshAsent;
    private ParametresUnit _parametresUnit;

    public string MoveAnim = "Move";

    private Random _rand = new Random();

    private bool _start = true;

    void Start()
    {
        _navMeshAsent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
        _parametresUnit = gameObject.GetComponent<ParametresUnit>();
        _iWG = gameObject.GetComponent<InteractWithGun>();
        _wait = new WaitForSeconds(_timeToRotate);
        StartCoroutine(rotf());
    }
 
    void Update()
    {
        if(_parametresUnit._isDead != true)
        {
            if (FindPriorityToMove() == false)
            {
                if (_start == true)
                {
                    StartCoroutine(rotf());

                    _navMeshAsent.stoppingDistance = 0;
                    Move(_toMove.position);
                }

            }
        }

    }

    IEnumerator rotf()
    {
       _start = false;
       int f = _rand.Next(0, 2);

       switch (f)
       {
           case 0: { transform.LookAt(_left); break; }
           case 1: { transform.LookAt(_right); break; }
        
       }
        yield return _wait;
        _start = true;
    }

    void Move(Vector3 _target)
    {
        _animator.SetFloat(MoveAnim, _navMeshAsent.velocity.magnitude);
        _navMeshAsent.destination = _target;
    }

    void RoundPlayer(Vector3 _target, StatePriority _stat)
    {
        if(_stat == StatePriority.FindPlayer)
        {           
            _iWG.Shooting(_parametresUnit.Gun.transform.GetChild(0).position);
            float Distance = Vector3.Distance(transform.position, _target);

            if (Distance > _limitTostop)
            {
                _navMeshAsent.stoppingDistance = _limitTostop;
                Move(_target);
            }
            transform.LookAt(_target);
        }
        else
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree 0"))
                _animator.Play("Blend Tree 0");

            _navMeshAsent.stoppingDistance = 0;
            Move(_target);
        }
       
    }

    private bool FindPriorityToMove()
    {
        List<StatePriority> list = new List<StatePriority>();
        List<Vector3> _target = new List<Vector3>();

        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _maskLayer);

        if (rangeChecks.Length > 0)
        {
            for(int i = 0;  i < rangeChecks.Length; i++)
            {
                if (rangeChecks[i].gameObject.layer == 11)
                {
                    Vector3 Direction = (rangeChecks[i].gameObject.transform.position - transform.position).normalized;
                    float Distance = Vector3.Distance(transform.position, rangeChecks[i].gameObject.transform.position);
                    if (!Physics.Raycast(transform.position, Direction, Distance, _obstical) && rangeChecks[i].gameObject.GetComponent<ParametresUnit>()._isDead == false)
                    {
                        _target.Add(rangeChecks[i].gameObject.transform.position);
                        list.Add(StatePriority.FindPlayer);
                    }
                }

                if (_parametresUnit._health < _parametresUnit._maxHealth)
                {
                    if (rangeChecks[i].gameObject.layer == 6)
                    {
                        Vector3 Direction = (rangeChecks[i].gameObject.transform.position - transform.position).normalized;
                        float Distance = Vector3.Distance(transform.position, rangeChecks[i].gameObject.transform.position);
                        if (!Physics.Raycast(transform.position, Direction, Distance, _obstical))
                        {
                            _target.Add(rangeChecks[i].gameObject.transform.position);
                            list.Add(StatePriority.FindHealth);
                        }
                    }
                }

                if (_parametresUnit._gun == null)
                {
                    if (rangeChecks[i].gameObject.layer == 7)
                    {
                        Vector3 Direction = (rangeChecks[i].gameObject.transform.position - transform.position).normalized;
                        float Distance = Vector3.Distance(transform.position, rangeChecks[i].gameObject.transform.position);
                        if (!Physics.Raycast(transform.position, Direction, Distance, _obstical) && Vector3.Angle(transform.forward, Direction) < _angleLimit / 2)
                        {
                            _target.Add(rangeChecks[i].gameObject.transform.position);
                            list.Add(StatePriority.FindGun);
                        }
                    }
                }


            }

        }

        if(list.Count > 0)
        {
            
            int st = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (i != 0)
                {
                    if (list[i] > list[st])
                    {
                        st = i;
                    }
                }
            }
            RoundPlayer(_target[st], list[st]);
            return true;
        }
        else
        {
            return false;
        }
            

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            StartCoroutine(rotf());
        }
    }

}
