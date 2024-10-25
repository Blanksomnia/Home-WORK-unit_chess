using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Collect : MonoBehaviour, IScore, IStatusVariable
{
    [SerializeField] TextMeshProUGUI Iscore;
    [SerializeField] int LimitScoreToReturn;
    private NavMeshAgent _navMeshAsent;
    [SerializeField] ChoiceActivate _choiceActivate;
    Transform _nearMushroom = null;
    bool _activate = false;
    Animator _animator;
    int _score = 0;

    void Start()
    {
        _navMeshAsent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_activate == true)
        {
            Move();
        }
    }

    void Move()
    {
        _animator.SetFloat("RUN", _navMeshAsent.velocity.magnitude);
        _navMeshAsent.destination = _nearMushroom.position;
    } 

    public void Activate(Transform transf)
    {
        _animator.Play("Blend Tree");
        _nearMushroom = transf;
        _activate = true;
    }
    IEnumerator DeleteMushroom()
    {
        _animator.Play("Gathering Objects");
        yield return new WaitForSeconds(2);
        if (_nearMushroom != null)
        {
            Destroy(_nearMushroom.gameObject);
            _nearMushroom = null;
            _score += 1;
            ScoreResult(Iscore, _score);
            if (_score >= LimitScoreToReturn)
            {
                _choiceActivate.Ñhoice(status.Return, null);
            }
            else
            {
                _choiceActivate.Ñhoice(ChooseStatus(), null);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 && _activate == true)
        {
            _activate = false;
            StartCoroutine(DeleteMushroom());
        }
    }
    public status ChooseStatus()
    {
        return status.Idle;
    }

    public void ScoreResult(TextMeshProUGUI TextScore, int Score)
    {
        TextScore.text = Convert.ToString(Score);
    }
}
