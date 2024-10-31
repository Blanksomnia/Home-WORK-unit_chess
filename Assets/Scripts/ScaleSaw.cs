using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSaw : MonoBehaviour
{
    [SerializeField] Vector3 _minScale;
    [SerializeField] Vector3 _maxScale;
    [SerializeField] float _duration;

    private Vector3 _endScale;
    private WaitForSeconds _TimeToChange;

    void Start()
    {
        _endScale = _maxScale;
        _TimeToChange = new WaitForSeconds(_duration);
        Startcoroutine();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 2));
    }

    void Startcoroutine()
    {
        StartCoroutine(ÑhangeOfScale());
    }

    private IEnumerator ÑhangeOfScale()
    {
        yield return _TimeToChange;

        if (_endScale == _minScale)
        {
            _endScale = _maxScale;
            transform.DOScale(_endScale, _duration);
        }
        else if (_endScale == _maxScale)
        {
            _endScale = _minScale;
            transform.DOScale(_endScale, _duration);
        }

        Startcoroutine();
    }
}
