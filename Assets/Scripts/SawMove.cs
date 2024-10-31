using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMove : MonoBehaviour
{
    [SerializeField] Vector3 _upMove;
    [SerializeField] Vector3 _downMove;

    [SerializeField] float _delay;
    [SerializeField] float _duration;

    private Vector3 EndMove;
    private WaitForSeconds _TimeToChange;
    void Start()
    {
        EndMove = _downMove;
        _TimeToChange = new WaitForSeconds(_delay);
        Startcoroutine();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 2));
    }

    void Startcoroutine()
    {
        StartCoroutine(ÑhangeOfMovement());
    }

    private IEnumerator ÑhangeOfMovement()
    {
        yield return _TimeToChange;

        if (EndMove == _upMove)
        {
            EndMove = _downMove;
            transform.DOMove(_downMove, _duration);
        }
        else if(EndMove == _downMove)
        {
            EndMove = _upMove;
            transform.DOMove(_upMove, _duration);
        }

        Startcoroutine();
    }
}
