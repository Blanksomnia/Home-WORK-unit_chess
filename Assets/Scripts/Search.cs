using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Search : MonoBehaviour, IStatusVariable
{
    [SerializeField] private LayerMask _mushroom;
    private NavMeshAgent _navMeshAsent;
    [SerializeField] ChoiceActivate _choiceActivate;
    Animator _animator;
    bool _activate = false;
    private System.Random _rand = new System.Random();
    private Vector3 _position;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _navMeshAsent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (_activate == true)
        {
            SearchMushrooms();
            Move(_position);

        }
    }

    IEnumerator TimeToScroll()
    {
        yield return new WaitForSeconds(4);
        ScrollTurn();
    }

    void ScrollTurn()
    {
        int Move = _rand.Next(0, 8);
        switch (Move) 
        {
            case 0: _position = transform.position + new Vector3(0, 0, 10); break;
            case 1: _position = transform.position + new Vector3(10, 0, 0); break;
            case 2: _position = transform.position + new Vector3(-10, 0, 0); break;
            case 3: _position = transform.position + new Vector3(0, 0, -10); break;
            case 4: _position = transform.position + new Vector3(10, 0, 10); break;
            case 5: _position = transform.position + new Vector3(-10, 0, -10); break;
            case 6: _position = transform.position + new Vector3(-10, 0, 10); break;
            case 7: _position = transform.position + new Vector3(10, 0, -10); break;
            default: break;
        }
        StartCoroutine(TimeToScroll());
    }

    void Move(Vector3 vec)
    {
        _animator.SetFloat("RUN", _navMeshAsent.velocity.magnitude);
        _navMeshAsent.destination = vec;
    }

    void SearchMushrooms()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 5, _mushroom);

        if (rangeChecks.Length > 0)
        {
            Transform Mushroom = null;
            for (int i = 0; i < rangeChecks.Length; i++)
            {

                float Distance = Vector3.Distance(transform.position, rangeChecks[i].transform.position);
                if (Mushroom != null)
                {
                    if (Vector3.Distance(transform.position, Mushroom.position) > Distance)
                    {
                        Mushroom = rangeChecks[i].transform;
                    }
                }
                else
                {
                    Mushroom = rangeChecks[i].transform;
                }


            }

            if (Mushroom != null)
            {
                _activate = false;
                _choiceActivate.Ñhoice(ChooseStatus(), Mushroom);
            }

            
        }
    }

    public void Activate()
    {
        ScrollTurn();
        _animator.Play("Blend Tree");
        _activate = true;
    }

    public status ChooseStatus()
    {
        return status.Collect;
    }

}
