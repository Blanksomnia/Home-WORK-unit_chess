using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.CanvasScaler;

public class ManagerUnits : MonoBehaviour, IManageUnits
{

    [SerializeField] Transform parentUnits;
    [SerializeField] Transform parentMines;
    public LayerMask ground;
    public LayerMask point;
    public LayerMask obsticalGround;
    public LayerMask obsticalPoint;

    ListUnitSpawner unitSpawner;
    List<MinePoint> mines = new List<MinePoint>();
    List<List<IStateUnitBehaviour>> units = new List<List<IStateUnitBehaviour>>();
    List<List<IStateUnitBehaviour>> exUnits = new List<List<IStateUnitBehaviour>>();
    List<IStateUnitBehaviour> selected = new List<IStateUnitBehaviour>();
    int maxUnitsLimit = 25;

    Camera camera;
    MaterialsManager managerMat;

    [HideInInspector] public int _maxUnitsLimit => maxUnitsLimit;
    [HideInInspector] public List<List<IStateUnitBehaviour>> _activities => units;
    [HideInInspector] public List<IStateUnitBehaviour> _selected => selected;

    CreateBase baseForUnit;

    [Inject]
    public void Construct(Camera CAM, ListUnitSpawner list, CreateBase baseB, MaterialsManager manager)
    {
        camera = CAM;
        unitSpawner = list;
        baseForUnit = baseB;
        managerMat = manager;
    }

    private void Awake()
    {
        foreach (MinePoint mine in parentMines.GetComponentsInChildren<MinePoint>())
        {
            mines.Add(mine);
        }
    }

    private void Start()
    {
        CreateUnit(TypeUnits.Worker, maxUnitsLimit);
        CreateUnit(TypeUnits.Knight, maxUnitsLimit);
    }

    public void CreateUnit(TypeUnits _type, int count)
    {
        GameObject unit = unitSpawner.UnitCreate(_type);

        for (int i = 0; i < count; i++)
        {
            IStateUnitBehaviour en = Instantiate(unit, parentUnits).GetComponent<IStateUnitBehaviour>();
            en.GetManager(this);
            AddExUnit(en);
        }
    }


    private void CheckType(TypeUnits type)
    {
        if (ID(type) > exUnits.Count - 1)
        {
            int mount = ID(type) - (exUnits.Count - 1);

            for (int i = 0; i < mount; i++)
            {
                exUnits.Add(new List<IStateUnitBehaviour>());
                units.Add(new List<IStateUnitBehaviour>());
            }

        }
    }

    public void SelectGroup()
    {
        if(selected.Count > 0)
        {

            for (int i = 0; i < _activities[ID(selected[0]._type())].Count; i++)
            {
                if (_activities[ID(selected[0]._type())][i]._type() == selected[0]._type() && selected[0] != _activities[ID(selected[0]._type())][i])
                {
                    SelectUnit(_activities[ID(selected[0]._type())][i]);
                }
            }
        }

    }

    public void SelectUnit(IStateUnitBehaviour unit)
    {
        CheckType(unit._type());

            if(selected.Count > 0)
            {
                if (unit._type() != selected[0]._type())
                {
                    ClearSelected();
                }
            }
            selected.Add(unit);
            unit.Select();
    }

    public void ClearSelected()
    {
        if(selected.Count > 0)
        {
            for (int i = 0; i < selected.Count; i++)
            {
                selected[i].Unselect();
            }

            selected.Clear();
        } 
    }



    private void AddExUnit(IStateUnitBehaviour unit)
    {
        if(unit._state() != StateUnit.Dead)
        {
            unit.IsDead();
        }

        CheckType(unit._type());
        unit._transform().position = Vector3.zero;
        exUnits[ID(unit._type())].Add(unit);
    }

    public void KillSelectedUnits()
    {
        if (selected.Count > 0)
        {
            for (int i = 0; i < selected.Count; i++)
            {
                selected[i].Unselect();
                KillUnit(selected[i]);
            }
            selected.Clear();
        }
        
    }

    public void KillUnit(IStateUnitBehaviour unit)
    {
        TypeUnits f = unit._type();
        CheckType(f);
        int IDun = units[ID(f)].Count + 5;
        for (int i = 0; i < units[ID(f)].Count; i++)
        {
            if (units[ID(f)][i] == unit)
            {

                IDun = i ;
            }
        }

        if(IDun != units[ID(f)].Count + 5)
        {
            AddExUnit(units[ID(f)][IDun]);
            units[ID(f)].RemoveAt(IDun);
            Debug.Log("Unit Deleted!!!");
            
        }
        else
        {
            Debug.Log("error!!!");
        }
    
    }

    public void addUnit(Vector3 pos, TypeUnits type)
    {


            CheckType(type);

            if (units[ID(type)].Count < maxUnitsLimit)
            {
                units[ID(type)].Add(exUnits[ID(type)][0]);
                exUnits[ID(type)][0]._transform().position = pos;
                exUnits[ID(type)][0].WakeUp();
                exUnits[ID(type)].RemoveAt(0);
                Debug.Log("Unit Added!!!");
                
            }
            else
            {
                
                Debug.Log("Units is full!!!");
            }

    }


    public int ID(TypeUnits type)
    {
        return Convert.ToInt32(type);
    }


    private void MoveToPointMine(MinePoint mine)
    {
        for (int i = 0; i < selected.Count; i++)
        {
            if (selected[i]._type() == TypeUnits.Worker)
            {
                selected[i].CollectResources(mine, baseForUnit._baseBuild, managerMat);
            }
        }
    }

    private void PointMine(Vector3 pos)
    {
        if(mines.Count > 0)
        {
            MinePoint mine = mines[0];
            for (int i = 0; i < mines.Count; i++)
            {
                float distCorrect = Vector3.Distance(mines[i].transform.position, pos);
                float distNearby = Vector3.Distance(mine.transform.position, pos);
                
                if(distCorrect < distNearby)
                {
                    mine = mines[i];
                }
            }
            MoveToPointMine(mine);
        }
       
    }

    private void UnitsMove(Vector3 vec)
    {
        for (int i = 0; i < selected.Count; i++)
        {
            selected[i].Move(vec);
        }
    }

    public void UnitMoveTo()
    {

        if (selected.Count > 0)
        {
            Vector3 vec = MousePoint(point, obsticalPoint);

            if (vec != Vector3.zero)
            {
                PointMine(vec);
            }
            else
            {

                vec = MousePoint(ground, obsticalGround);
                if (vec != Vector3.zero)
                {
                    UnitsMove(vec);
                }
            }

        }

    }

    public Vector3 MousePoint(LayerMask layer, LayerMask obst)
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());


        if (!Physics.Raycast(ray, out hit, 250, obst))
        {
            hit = new RaycastHit();

            if(Physics.Raycast(ray, out hit, 250, layer)) { return hit.point; }
            return Vector3.zero;
        }
        else
        {
            return Vector3.zero;
        }

    }

    private void Update()
    {
        for (int i = 0; i < _activities.Count; i++)
        {
            for (int j = 0; j < _activities[i].Count; j++)
            {
                _activities[i][j].StateUpdate();
            }
        }

    }
}
