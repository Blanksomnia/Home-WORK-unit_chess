using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ToolMovement : MonoBehaviour
{
    [SerializeField] float _SpeedRotate;
    [SerializeField] float _SpeedMove;

    [SerializeField] Vector3 _posMove;
    [SerializeField] Vector3 _posRotate;

    [SerializeField] float _delay;

    private Vector3 _posUp;
    private Vector3 _posDown;

    private bool TurnVec = true;

    private WaitForSeconds _waitforseconds;

    private void Awake()
    {
        _posUp = transform.position + _posMove;
        _posDown = transform.position - _posMove;
        StartCoroutine(ChangePos());
        _waitforseconds = new WaitForSeconds(_delay);
    }


    private void Update()
    {
        transform.Rotate(_posRotate, _SpeedRotate * Time.deltaTime);

        transform.position = TurnVec ? Vector3.Lerp(transform.position, _posUp, _SpeedMove * Time.deltaTime) : Vector3.Lerp(transform.position, _posDown, _SpeedMove * Time.deltaTime);
    }

    private IEnumerator ChangePos()
    {
        yield return _waitforseconds;
        TurnVec = TurnVec ? false : true;
        StartCoroutine(ChangePos());
    }
}
