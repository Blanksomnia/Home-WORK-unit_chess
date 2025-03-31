using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Zenject;
using static UnityEngine.GraphicsBuffer;

public class KnightCharacterBehaviour : MonoBehaviour, IStateUnitBehaviour
{
    private ManagerUnits mover;
    [SerializeField] Attacker attacker;
    WaitForSeconds wait = new WaitForSeconds(1);
    float timerDuration = 0;
    float timerDead = 0;
    IStateUnitBehaviour target = null;
    ICharacterAnimatorListener anim;

    CharacterEntity character;
    NavMeshAgent navMeshAgent;

    float maxDistanceToPoint = 0.5f;
    float maxDistanceToUnit = 1.5f;
    bool onPoint = false;
    Vector3 pos = new Vector3();
    private StateUnit state;

    int damage = 2;
    int duration = 2;
    float radius = 1;
    float radiusAttack = 1;
    LayerMask targets;
    LayerMask obstacle;

    [SerializeField] MaterialUnit mat;
    Material enter;
    Material select;
    Material exit;
    SkinnedMeshRenderer meshRenderer;
    private bool _select = false;
    private bool canAttack = true;
    public Transform _transform() => transform;
    public TypeUnits _type() => character._type;
    public void GetDamage(int damage) { character.GetDamage(damage); if (character._health <= 0) {  StartDead(); } }
    public StateUnit _state() => state;
    public bool _onPoint() => onPoint;
    public Vector3 _pos() => pos;

    private void Awake()
    {
        enter = mat.enter;
        select = mat.select;
        exit = mat.exit;

        foreach (SkinnedMeshRenderer t in transform.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            meshRenderer = t;
        }

        character = GetComponent<CharacterEntity>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = character._speed;
        anim = GetComponent<ICharacterAnimatorListener>();
        damage = attacker.damage;
        duration = attacker.duration;
        radius = attacker.radius;
        radiusAttack = attacker.radiusAttack;
        targets = attacker.targets;
        obstacle = attacker.obstacle;
    }

    public void GetManager(ManagerUnits manage) => mover = manage;

    private void ChangeMat(Material mat)
    {
        if (mat == null)
        {
            meshRenderer.material = exit;
        }
        meshRenderer.material = mat;
    }

    public void OnPointerEnter()
    {
        if (_select == false)
            ChangeMat(enter);
    }

    public void OnPointerExit()
    {
        if (_select == false)
            ChangeMat(exit);
    }

    public void Select()
    {
        ChangeMat(select);

        _select = true;
    }

    public void Unselect()
    {
        ChangeMat(exit);
        _select = false;
    }

    public void WakeUp() => WakeUpUnit();

    private void WakeUpUnit()
    {
        character.ClearHealth();
        gameObject.SetActive(true);
        Stay();
    }

    private void StartMove(Vector3 posit)
    {
        navMeshAgent.stoppingDistance = 0;
        pos = posit;
        navMeshAgent.speed = character._speed;
        anim.move();
        onPoint = false;
        navMeshAgent.SetDestination(pos);
        state = StateUnit.Move;

    }

    private void StartAngry()
    {
        navMeshAgent.speed = 0;
        anim.stay();
        state = StateUnit.AngryUnits;
    }

    public void StateUpdate()
    {


        if (state == StateUnit.Move)
        {
            MoveUnit();
        }

        if (state == StateUnit.AngryUnits)
        {
            UnitAngry();
        }
    }


    public void Stay() => StartAngry();
    public void Move(Vector3 posit) => StartMove(posit);
    public void CollectResources(MinePoint mineP, Transform posBase, MaterialsManager material) => StartAngry();
    public void AngryToUnits() => StartAngry();
    public void IsDead() => DeadUnit();


    private void MoveUnit()
    {
        Move();
        FindTarget();

    }

    private void Move()
    {
        if (Vector3.Distance(character.transform.position, pos) <= maxDistanceToPoint)
        {
            onPoint = true;
            Stay();
        }
        else
        {
            for (int i = 0; i < mover._activities[mover.ID(character._type)].Count; i++)
            {
                if (mover._activities[mover.ID(character._type)][i]._onPoint() == true && mover._activities[mover.ID(character._type)][i]._pos() == pos)
                {
                    if (Vector3.Distance(character.transform.position, mover._activities[mover.ID(character._type)][i]._transform().position) <= maxDistanceToUnit)
                    {
                        onPoint = true;
                        Stay();
                    }
                }

            }
        }
    }

    private void UnitAngry()
    {

        if (target != null)
        {

                if (CanAttack())
                {
                    AttackTarget();
                }
                else
                {

                    if (Vector3.Distance(target._transform().position, transform.position) > radiusAttack)
                        MoveToTarget();
                }

        }
        else
        {

            FindTarget();
        }
    }

    public bool CanAttack() { 
        if (canAttack &&
        Vector3.Distance(target._transform().position, transform.position) <= radiusAttack && target._state() != StateUnit.Dead)
        { return true; } else { return false; } 
    }

    private IEnumerator TimerAttack()
    {
        yield return wait;
        timerDuration += 1;
        if (timerDuration <= duration) { StartCoroutine(TimerAttack()); } else { timerDuration = 0; canAttack = true; }
    }

    private IEnumerator TimerDead()
    {
        yield return wait;
        timerDead += 2;
        if (timerDead <= 3) { StartCoroutine(TimerDead()); } else { timerDead = 0; if (state == StateUnit.Dead) { DeadUnit(); } }
    }

    private void StartMoveToTarget()
    {
        navMeshAgent.speed = character._speed;
        anim.move();
        navMeshAgent.stoppingDistance = radiusAttack - 2f;
    }

    private void MoveToTarget() 
    {
        if (target._state() == StateUnit.Dead)
        {
            target = null;

            FindTarget();

            if (target == null)
            {
                Stay();
            }
        }
        else
        {
            navMeshAgent.SetDestination(target._transform().position);
        }
    }

    private void AttackTarget() 
    {
        navMeshAgent.speed = 0;
        transform.LookAt(target._transform());
        anim.attack();
        canAttack = false;
        StartCoroutine(TimerAttack());
        target.GetDamage(damage);

    }

    private void FindTarget()
    {
        
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targets);

        if (rangeChecks.Length > 0)
        {
            
            List<Transform> characters = new List<Transform>();
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Vector3 direct = (rangeChecks[i].transform.position - character.transform.position).normalized;
                float distance = Vector3.Distance(character.transform.position, rangeChecks[i].transform.position);
                if (!Physics.Raycast(transform.position, direct, distance, obstacle))
                {
                    characters.Add(rangeChecks[i].transform);
                }
            }

            Transform nearbyChar = characters[0];
            for(int i = 0;i < characters.Count; i++)
            {
                    float DistCorrect = Vector3.Distance(transform.position, characters[i].position);
                    float DistNerby = Vector3.Distance(transform.position, nearbyChar.position);

                    if (DistCorrect < DistNerby)
                    {
                        nearbyChar = characters[i];
                    }
            }

            for (int i = 0; i < mover._activities.Count; i++)
            {
                for (int j = 0; j < mover._activities[i].Count; j++)
                {
                    if (mover._activities[i][j]._transform() == nearbyChar )
                    {
                        StartMoveToTarget();
                        target = mover._activities[i][j];

                    }


                }
            }
        }

    }


    private void StartDead()
    {
        state = StateUnit.Dead;
        anim.StartDeathAnim();
        StartCoroutine(TimerDead());
    }

    private void DeadUnit()
    {
        state = StateUnit.Dead;
        mover.KillUnit(this);
        gameObject.SetActive(false);
    }
}
