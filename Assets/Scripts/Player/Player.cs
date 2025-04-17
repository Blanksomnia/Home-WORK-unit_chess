using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class Player : MonoBehaviour
{
    AnimationPlayer _animations;
    HitBoxCharacter character;
    MovementPlayer mover;

    bool _inAir = false;
    bool _canClimb = false;
    public bool LockJump = false;
    public bool LockMove = false;
    public float speedDefault = 2f;
    public float speedInAir = 0.001f;
    public float powerJump;
    public Vector2 turnMove;
    public Vector2 turnToJumpAfterClimb;


    List<HitBoxCharacter> enemies = new List<HitBoxCharacter>();
    public HitBoxCharacter enemy;

    public LayerMask layerGround;
    [SerializeField] LayerMask unit;
    [SerializeField] Transform collectUnits;
    [SerializeField] int _damage;

    BoxCollider _colliderClimb;
    BoxCollider _colliderJump;
    Rigidbody _rb;
    CapsuleCollider _capsuleCollider;

    public Rigidbody rb => _rb;
    public BoxCollider colliderClimb => _colliderClimb;
    public CapsuleCollider capsuleCollider => _capsuleCollider;
    public int damage => _damage;
    public bool inAir => _inAir;
    public bool canClimb => _canClimb;
    public AnimationPlayer animations => _animations;

    private void Awake()
    {
        character = GetComponent<HitBoxCharacter>();
        _rb = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();

        foreach (ColliderJump collider in rb.transform.GetComponentsInChildren<ColliderJump>())
        {
            _colliderJump = collider.GetComponent<BoxCollider>();
        }
        foreach (ColliderClimb collider in rb.transform.GetComponentsInChildren<ColliderClimb>())
        {
            _colliderClimb = collider.GetComponent<BoxCollider>();
        }

        foreach (HitBoxCharacter hit in collectUnits.GetComponentsInChildren<HitBoxCharacter>())
        {

            if(character != hit)
            enemies.Add(hit);
        }

        _animations = new AnimationPlayer(this);

        mover = new MovementPlayer();
        mover.AddState(new IdleState(this, StatePlayer.Idle));
        mover.AddState(new MovementState(this, StatePlayer.Movement));
        mover.AddState(new JumpState(this, StatePlayer.Jump));
        mover.AddState(new ClimbState(this, StatePlayer.Climb));
        mover.AddState(new AttackState(this, StatePlayer.AttackInAir));
        mover.AddState(new DeadState(this, StatePlayer.Dead));

    }


    private void Update()
    {
        _animations.Update();
        Checks();
        mover.Update();
    }

    private void AnimJump()
    {
        if (_inAir == true) { animations.StartJump(); } else { animations.EndJump(); LockMove = false; }
    }
    private void AnimClimb()
    {
        if (_canClimb == true)
        {
            Rotation();
            rb.isKinematic = true;
            animations.StartClimb();
        }
        else { }

    }

    private void Rotation()
    {
        Vector3 posPlayer = rb.transform.position;
        Vector3 rightEndDirection = rb.transform.forward + (rb.transform.right * 0.2f);
        Vector3 leftEndDirection = rb.transform.forward - (rb.transform.right * 0.2f);
        Vector3 hitLeft = Vector3.zero;
        Vector3 hitRight = Vector3.zero;

        if (Physics.Raycast(posPlayer, leftEndDirection, out RaycastHit L, 5, layerGround))
        {
            hitLeft = L.point;
        }
        if (Physics.Raycast(posPlayer, rightEndDirection, out RaycastHit R, 5, layerGround))
        {
            hitRight = R.point;
        }

        Quaternion rotationForward = Quaternion.Euler(new Vector3(0, -180, 0) + Quaternion.FromToRotation(Vector3.right, hitLeft - hitRight).eulerAngles);
        Vector3 start = (rotationForward * Vector3.back) + hitRight;

        if (hitLeft != Vector3.zero && hitRight != Vector3.zero)
        {
            animations.LookAt(hitRight - start + rb.transform.position, 200);
        }

    }


    private void Checks()
    {
        if (character._health == 0)
        {
            mover.state = StatePlayer.Dead;
        }
        else
        {
            bool air = CheckAir();

            if(air != _inAir)
            {
                _inAir = air;
                AnimJump();
            }

            bool climb = CheckCanClimb();

            if (climb != _canClimb)
            {
                
                _canClimb = climb;
                AnimClimb();
            }


            triggerCharacterEnter();

            if (turnMove == Vector2.zero)
            {
                mover.state = StatePlayer.Idle;
            }
            else if (turnMove != Vector2.zero && !canClimb && !LockMove)
            {
                mover.state = StatePlayer.Movement;
            }
            else if (inAir && _canClimb && turnMove != Vector2.zero && !LockMove)
            {
                mover.state = StatePlayer.Climb;
            }
            else if (inAir && enemy != null && !canClimb)
            {
                mover.state = StatePlayer.AttackInAir;
            }

        }

    }




    public void Move(Vector2 turn)
    {

        turnMove = turn;

    }

    public void Jump()
    {
        if (!LockJump)
        {

                mover.state = StatePlayer.Jump;

        }

    }

    private void triggerCharacterEnter()
    {
        if(enemies.Count > 0)
        {
            Collider[] colliders = Physics.OverlapBox(_colliderJump.transform.position, _colliderJump.size, Quaternion.identity, unit);
            if (colliders.Length > 0)
            {

                int index = -1;

                for (int i = 0; i < colliders.Length; i++)
                {
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        if (colliders[i].transform == enemies[j].transform)
                        {
                            index = j; break;
                        }

                    }
                }


                if (index != -1)
                {
                    enemy = enemies[index];
                }
            }
        }

    }

    private bool CheckAir()
    {
        if (!Physics.CheckBox(_colliderJump.transform.position, _colliderJump.size, Quaternion.identity, layerGround))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool CheckCanClimb()
    {

        if (!Physics.CheckBox(_colliderClimb.transform.position, _colliderClimb.size, Quaternion.identity, layerGround))
        {
            return false;
        }
        else
        {
            if (!Physics.CheckBox(_colliderClimb.transform.position + rb.transform.forward * _colliderClimb.size.z + new Vector3(0, _colliderClimb.size.y, 0), _colliderClimb.size, Quaternion.identity, layerGround) && _inAir)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
