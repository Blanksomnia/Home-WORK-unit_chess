using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacterBehaviour : MonoBehaviour, IStateUnitBehaviour
{

    private IListUnits mover;
    [SerializeField] Attacker attacker;
    WaitForSeconds wait = new WaitForSeconds(1);
    float timerDuration = 0;
    float timerDead = 0;
    IStateUnitBehaviour target = null;
    ICharacterAnimatorListener anim;

    CharacterEntity character;
    NavMeshAgent navMeshAgent;

    float maxDistanceToPoint = 5f;
    float maxDistanceToUnit = 1.5f;
    bool onPoint = true;
    Vector3 pos = new Vector3();
    private StateUnit state;

    int damage = 2;
    int duration = 2;
    float radius = 1;
    float radiusAttack = 1;
    LayerMask targets;
    LayerMask obstacle;


    private Vector3 startPos;

    private bool canAttack = true;
    public Transform _transform() => transform;
    public TypeUnits _type() => character._type;
    public void GetDamage(int damage) { character.GetDamage(damage); if (character._health <= 0) {  StartDead(); } }

    public CharacterEntity _character() => character;
    public StateUnit _state() => state;
    public bool _onPoint() => onPoint;
    public Vector3 _pos() => pos;

    private void Awake()
    {
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

    public void GetManager(IListUnits manage) => mover = manage;


    public void OnPointerEnter() { }

    public void OnPointerExit() { }

    public void Select() { }

    public void Unselect() { }

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

        if(pos == new Vector3())
        {
            pos = posit;
        }
        navMeshAgent.speed = character._speed;
        anim.move();
        onPoint = false;
        navMeshAgent.SetDestination(pos);
        state = StateUnit.Move;

    }

    private void StartAngry()
    {
        onPoint = true;
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
    public void CollectResources(MinePoint mineP, Transform posBase, MaterialsManager material) => Empty();
    public void IsDead() => DeadUnit();


    private void Empty() { }

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
            for (int i = 0; i < mover._activities()[2].Count; i++)
            {
                if (mover._activities()[2][i]._onPoint() == true)
                {
                    if (Vector3.Distance(character.transform.position, mover._activities()[2][i]._transform().position) <= maxDistanceToUnit)
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

    public bool CanAttack()
    {
        if (canAttack &&
        Vector3.Distance(target._transform().position, transform.position) <= radiusAttack && target._state() != StateUnit.Dead)
        { return true; }
        else { return false; }
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
        timerDead += 1;
        if (timerDead <= 2) { StartCoroutine(TimerDead()); } else { timerDead = 0;  DeadUnit();  }
    }

    private void StartMoveToTarget()
    {
        startPos = transform.position;
        onPoint = false;
        navMeshAgent.speed = character._speed;
        anim.move();
        navMeshAgent.stoppingDistance = radiusAttack - 2f;
    }

    private void MoveToTarget()
    {
        if(target._state() == StateUnit.Dead)
        {
            
            target = null;

            FindTarget();

            if(target == null)
            {
                if (Vector3.Distance(character.transform.position, pos) <= maxDistanceToPoint)
                {
                    
                    Stay();
                }
                else
                {
                    StartMove(startPos);
                }
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
            for (int i = 0; i < characters.Count; i++)
            {
                float DistCorrect = Vector3.Distance(transform.position, characters[i].position);
                float DistNerby = Vector3.Distance(transform.position, nearbyChar.position);

                if (DistCorrect < DistNerby)
                {
                    nearbyChar = characters[i];
                }
            }

            for (int i = 0; i < mover._activities().Count; i++)
            {
                for (int j = 0; j < mover._activities()[i].Count; j++)
                {
                    if (mover._activities()[i][j]._transform() == nearbyChar)
                    {
                        StartMoveToTarget();
                        target = mover._activities()[i][j];

                    }


                }
            }
        }

    }


    private void StartDead()
    {
        anim.StartDeathAnim();
        StartCoroutine(TimerDead());
    }

    private void DeadUnit()
    {
        state = StateUnit.Dead;
        gameObject.SetActive(false);
    }
}
