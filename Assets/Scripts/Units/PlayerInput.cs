using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class PlayerInput : MonoBehaviour
{
    float speed;

    Vector3 Move;
    [HideInInspector] public Animator _animator;
    Rigidbody rb;
    private InteractWithGun _iWG;
    private string MoveAnim = "Move";
    private ParametresUnit _parametresUnit;

    private void Awake()
    {
        _parametresUnit = GetComponent<ParametresUnit>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _iWG = gameObject.GetComponent<InteractWithGun>();
    }

    private void Update()
    {

        if (_parametresUnit._isDead != true)
        {
            Vector3 F = new Vector3();

            if (Input.GetKey(KeyCode.W))
                F = transform.forward;

            if (Input.GetKey(KeyCode.S))
                F = -transform.forward;

            if (Input.GetKey(KeyCode.A))
                F = -transform.right;

            if (Input.GetKey(KeyCode.D))
                F = transform.right;


            if (F == new Vector3())
            {
                _animator.SetFloat(MoveAnim, 0);
            }
            else
            {
                

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 5;
                    _animator.SetFloat(MoveAnim, 1);
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                    speed = 1;
                    _animator.SetFloat(MoveAnim, 0.5f);
                }
                transform.position += F * speed * Time.deltaTime;
            }


            

            if (Input.GetMouseButton(0))
            {
                if(_parametresUnit.Gun != null)
                {
                    _iWG.Shooting(_parametresUnit.Gun.transform.GetChild(0).position);
                }
            }
            else
            {
                if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Blend Tree 0"))
                _animator.Play("Blend Tree 0");
            }
            


        }
    }
   
}
