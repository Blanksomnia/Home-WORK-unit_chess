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

public class ManagerUnits : MonoBehaviour, IManageUnits, IListUnits
{

    [SerializeField] Transform parentUnits;
    [SerializeField] Transform parentMines;
    public LayerMask ground;
    public LayerMask obsticalGround;

    ListUnitSpawner unitSpawner;
    List<MinePoint> mines = new List<MinePoint>();
    List<List<IStateUnitBehaviour>> units = new List<List<IStateUnitBehaviour>>();
    List<List<IStateUnitBehaviour>> exUnits = new List<List<IStateUnitBehaviour>>();
    List<IStateUnitBehaviour> selected = new List<IStateUnitBehaviour>();
    int maxUnitsLimit = 25;

    Camera camera;

    MaterialsManager managerMat;

    private CreateBuild selectedBuild = null;
    private Transform posBase;
    List<Transform> patrols = new List<Transform>();

    public int _maxUnitsLimit => maxUnitsLimit;

    public List<List<IStateUnitBehaviour>> _activities() => units;
    public List<MinePoint> _minePoints() => mines;
    public Transform _posBase() => posBase;
    public MaterialsManager _materials() => managerMat;
    public List<Transform> _patrols() => patrols;
    public List<IStateUnitBehaviour> _selected => selected;


    [Inject]
    public void Construct(Camera CAM, ListUnitSpawner list, MaterialsManager manager)
    {
        camera = CAM;
        unitSpawner = list;
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


    public void KillSelectBuild()
    {
        selectedBuild = null;
    }

    public void SelectBuild(CreateBuild f)
    {
        if (selectedBuild != null)
        {
            selectedBuild.UnSelect();
        }


        selectedBuild = f;
    }

    public void CreateBuild(Vector3 pos)
    {
        if(selectedBuild != null)
        {
            if (posBase == null)
            {
                if (selectedBuild.nameBuild == "Base")
                {
                    selectedBuild.addBuild(pos);
                }
            }
            else
            {
                selectedBuild.addBuild(pos);
            }
        }

    }

    public void GetPatrol(Transform patrol)
    {
        patrols.Add(patrol);
    }

    public void GetBase(Transform build)
    {
        posBase = build;
    }

    public void CreateUnit(TypeUnits _type, int count)
    {
        GameObject unit = unitSpawner.UnitCreate(_type);

        for (int i = 0; i < count; i++)
        {
            GameObject un = Instantiate(unit, parentUnits);
            un.gameObject.SetActive(false);
            IStateUnitBehaviour en = un.GetComponent<IStateUnitBehaviour>();
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
        TypeUnits type = selected[0]._type();
        CheckType(type);
        selectedBuild = null;
        if (selected.Count > 0)
        {

            if (selected[0]._type() != type)
            {
                ClearSelected();
            }
            for (int i = 0; i < units[ID(type)].Count; i++)
            {
                SelectUnit(units[ID(type)][i]);
            }
        }
        else
        {
            for (int i = 0; i < units[ID(type)].Count; i++)
            {
                SelectUnit(units[ID(type)][i]);
            }
        }

    }

    public void SelectUnit(IStateUnitBehaviour unit)
    {
        CheckType(unit._type());

        selectedBuild = null;

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
            unit.IsDead();
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
            Vector3 vec = MousePoint(ground, obsticalGround);

            vec = MousePoint(ground, obsticalGround);
            if (vec != Vector3.zero)
            {
                UnitsMove(vec);
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
        for (int i = 0; i < units.Count; i++)
        {
            for (int j = 0; j < units[i].Count; j++)
            {
                if(units[i][j]._state() == StateUnit.Dead)
                {
                    KillUnit(units[i][j]);
                }
                else
                {
                    units[i][j].StateUpdate();
                }
            }
        }

        if (selectedBuild != null)
        {
            selectedBuild.Selected(MousePoint(ground, obsticalGround));
        }

    }
}
