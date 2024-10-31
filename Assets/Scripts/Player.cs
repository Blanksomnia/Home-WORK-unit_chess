using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class Player : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] private Transform _camera;

    [SerializeField] private float _durationMin;
    [SerializeField] private float _durationMax;

    [SerializeField] private Animator _animator;

    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector3 _wayCastlePlayer = new Vector3(-7.78f, -17.33f, 0);
    private Vector3 _wayNearCastlePlayer = new Vector3(-3.05f, -2.07f, 0);
    private Vector3 _wayOutsidePlayer = new Vector3(3.48f, -2.07f, 0);
    private Vector3 _wayDownPlayer = new Vector3(2.04f, -21.52f, 0);

    private Vector3 _Incastle = new Vector3(-6.45f, -17.33f, 0);
    private Vector3 _Outcastle = new Vector3(-1.64F, -2.07f, 0);

    private float _duration;

    private status _locate = status.Outside;
    private status _Tomove = status.None;

    private Vector3 _posCastlePlaceCamera = new Vector3(-0.36F, -17.95f, -10);
    private Vector3 _posOutsidePlaceCamera = new Vector3(-0.36F, -2f, -10);
    private void Start()
    {
        _duration = _durationMax;
    }
    void Update()
    {

        MoveRobot();
        AnimateStatus();
    }

    private void Movement(Vector3 pos)
    {
        //_duration используется как скорость
        transform.DOMove(pos, _duration);
    }

    private void AnimateStatus()
    {
        if (_playerInput.IsPressed == false && _Tomove != status.None)
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
            {
                _animator.Play("Move");
            }
            _duration = _durationMax;
        }
        else if (_playerInput.IsPressed == true && _Tomove != status.None)
        {
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                _animator.Play("Run");
            }
            _duration = _durationMin;
        }
    }

    private void MoveRobot()
    {
        float Distance = 0;
        float RGB = 0;
        switch (_Tomove)
        {
            case status.None: 
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
                { 
                    _animator.Play("Stay"); 
                }
                break;
            case status.NearCastle:
                if (_locate == status.Outside)
                {
                    _spriteRenderer.flipX = true;
                    Movement(_wayNearCastlePlayer);
                }
                else if(_locate == status.NearCastle)
                {
                    _locate = status.Castle;
                    _spriteRenderer.flipX = false;
                    Movement(_Incastle);
                    _Tomove = status.None;
                    _camera.position = _posCastlePlaceCamera;
                } 
                break;
            case status.Outside:
                    _spriteRenderer.flipX = false;
                Movement(_wayOutsidePlayer);
                break;
            case status.Castle:
                if (_locate == status.Down)
                {

                    _spriteRenderer.flipX = true;
                    Movement(_wayCastlePlayer);
                    Distance = Vector3.Distance(_wayCastlePlayer, transform.position);
                    RGB = _curve.Evaluate(1 - Distance / 10);
                    _spriteRenderer.color = new Color(RGB, RGB, RGB);

                }
                else if (_locate == status.Castle)
                {
                    _locate = status.NearCastle;
                    _spriteRenderer.flipX = true;
                    Movement(_Outcastle);
                    _Tomove = status.None;
                    _camera.position = _posOutsidePlaceCamera;
                }
                break;
            case status.Down:
                _spriteRenderer.flipX = false;
                Movement(_wayDownPlayer);
                Distance = Vector3.Distance(_wayCastlePlayer, transform.position);
                RGB = _curve.Evaluate(1 - Distance / 10);
                _spriteRenderer.color = new Color(RGB, RGB, RGB);
                break;
            default: break;

        }
    }

    public void MoveToCastle()
    {
        
        if(_Tomove == status.None)
        {
            _Tomove = status.Castle;
        }
    }

    public void MoveNearCastle()
    {
        if (_Tomove == status.None)
        {
            _Tomove = status.NearCastle;
        }
    }

    public void MoveToOutside()
    {
        if (_Tomove == status.None && _locate != status.Outside)
        {
            _Tomove = status.Outside;
        }
    }

    public void MoveDown()
    {
        if (_Tomove == status.None && _locate != status.Down)
        {
            _Tomove = status.Down;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Trigger>())
        {
            //DORewind() используеться чтобы прекрать движение
            if (_Tomove == status.Outside)
            {
                _locate = _Tomove;
                _Tomove = status.None;
            }

            if(_Tomove == status.NearCastle)
            {
                _locate = status.Castle;
                _Tomove = status.None;
                _spriteRenderer.flipX = false;
                _camera.position = _posCastlePlaceCamera;
                transform.DORewind();
                transform.position = _Incastle;
            }

            if(_Tomove == status.Castle)
            {
                _locate = status.NearCastle;
                _Tomove = status.None;
                _spriteRenderer.flipX = false;
                _camera.position = _posOutsidePlaceCamera;
                transform.DORewind();
                transform.position = _Outcastle;
            }

            if (_Tomove == status.Down)
            {
                _locate = _Tomove;
                _Tomove = status.None;
            }
        }
    }

}
