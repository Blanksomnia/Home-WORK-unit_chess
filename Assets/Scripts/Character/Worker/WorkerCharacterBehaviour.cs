using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;
using static UnityEditor.PlayerSettings;

public class WorkerCharacterBehaviour : MonoBehaviour, IStateUnitBehaviour
{
    private IListUnits mover;
    private MaterialMine material;
    WaitForSeconds wait = new WaitForSeconds(1);

    CharacterEntity character;
    NavMeshAgent navMeshAgent;
    ICharacterAnimatorListener anim;
    [SerializeField] MeshRenderer arms;
    private int resourceCount = 1;
    MinePoint point;
    bool withResource = false;
    float maxDistanceToPoint = 0.5f;
    float maxDistanceToUnit = 1.5f;
    bool onPoint = false;
    Vector3 pos = new Vector3();
    private StateUnit state;
    float timerDead = 0;
    [SerializeField] MaterialUnit mat;
    [SerializeField] LayerMask points;
    Material enter;
    Material select;
    Material exit;
    SkinnedMeshRenderer meshRenderer;
    private bool _select = false;

    public Transform _transform() => transform;
    public TypeUnits _type() => character._type;
    public void GetDamage(int damage) { character.GetDamage(damage); if (character._health <= 0) { StartDead(); } }
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
    }

    public void GetManager(IListUnits manage) => mover = manage;

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
        navMeshAgent.speed = character._speed;
        pos = posit;
        anim.move();

        Collider[] colP = Physics.OverlapSphere(posit, 3, points);
        if (colP.Length > 0)
        {
            print(colP[0]);
            for (int j = 0; j < colP.Length; j++)
            {
                for (int i = 0; i < mover._minePoints().Count; i++)
                {
                    if (colP[j].transform == mover._minePoints()[i].transform)
                    {
                        StartCollect(mover._minePoints()[i]);
                    }
                }
            }
        }
        else
        {
            if (withResource)
            {
                StartCollect(point);
            }
            else
            {
                onPoint = false;
                navMeshAgent.SetDestination(pos);
                state = StateUnit.Move;
            }
        }

    }

    private void StartCollect(MinePoint mineP)
    {
        navMeshAgent.speed = character._speed;
        anim.move();
        state = StateUnit.CollectResources;
        point = mineP;
        material = mover._materials().material(mineP.type);
        if (withResource)
        {
            navMeshAgent.SetDestination(mover._posBase().position);
        }
        else
        {
            navMeshAgent.SetDestination(point.transform.position);

        }
    }

    private void Collect()
    {
        if (material._value <= material._limit + resourceCount)
        {
            if (withResource)
            {
                if (CheckPos(mover._posBase().position))
                {
                    anim.move();
                    navMeshAgent.SetDestination(point.transform.position);
                    mover._materials().AddValue(point.type, resourceCount);
                    DeleteResource();
                    withResource = false;
                }
            }
            else
            {

                if (CheckPos(point.transform.position))
                {
                    anim.collect();
                    navMeshAgent.SetDestination(mover._posBase().position);
                    AddResource();
                    withResource = true;
                }
            }
        }
        else
        { Stay(); }

    }

    public void StateUpdate()
    {

        if (state == StateUnit.Move)
        {
            MoveUnit();
        }

        if (state == StateUnit.CollectResources)
        {
            Collect();
        }
    }



    public void Stay() => StayUnit();
    public void Move(Vector3 posit) => StartMove(posit);
    public void IsDead() => DeadUnit();

    private void StayUnit()
    {
        DeleteResource();
        anim.stay();
        state = StateUnit.Stay;
        navMeshAgent.speed = 0;

    }

    private void MoveUnit()
    {

        if (Vector3.Distance(character.transform.position, pos) <= maxDistanceToPoint)
        {
            onPoint = true;
            Stay();
        }
        else
        {
            for (int i = 0; i < mover._activities()[0].Count; i++)
            {

                if (mover._activities()[0][i]._onPoint() == true && mover._activities()[0][i]._pos() == pos)
                {
                    if (Vector3.Distance(character.transform.position, mover._activities()[0][i]._transform().position) <= maxDistanceToUnit)
                    {

                        onPoint = true;
                        Stay();
                    }
                }

            }
        }

    }


    private bool CheckPos(Vector3 pos)
    {
        if(Vector3.Distance(transform.position, pos) <= 3)
        {
            return true;
        }
        else {  return false; }
    }

    private void DeleteResource()
    {
        arms.gameObject.SetActive(false);

    }

    private void AddResource()
    {
        arms.material = point.mineMat;
        arms.gameObject.SetActive(true);
    }


    private IEnumerator TimerDead()
    {
        yield return wait;
        timerDead += 1;
        if (timerDead <= 2) { StartCoroutine(TimerDead()); } else { timerDead = 0; DeadUnit(); }
    }

    private void StartDead()
    {
        anim.StartDeathAnim();
        StartCoroutine(TimerDead());
    }

    private void DeadUnit()
    {
        DeleteResource();
        state = StateUnit.Dead;
        gameObject.SetActive(false);
    }

}
