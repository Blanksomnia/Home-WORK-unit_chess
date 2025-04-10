using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;
using static UnityEditor.PlayerSettings;

public class InventoryUnits : MonoBehaviour, IInventoryUnits
{
    ManagerUnits manageUnits;
    CellUnit selected = null;

    [Inject]
    public void Construct(ManagerUnits manage)
    {
        manageUnits = manage;
    }

    public void GetSelect(CellUnit s)
    {
        selected = s;
    }

    public void AddSelected(int value)
    {
        Vector3 pos = manageUnits.MousePoint(manageUnits.ground, manageUnits.obsticalGround);
        if(pos != Vector3.zero)
        {
            if (manageUnits._posBase() != null)
            {
                manageUnits.CreateBuild(pos);

                if(manageUnits._activities().Count > 0 && selected != null)
                {
                    if (manageUnits._activities()[manageUnits.ID(selected._type)].Count + value <= manageUnits._maxUnitsLimit)
                        if (CheckAddUnit(value))
                        {
                            manageUnits.addUnit(pos, selected._type);
                            selected.RemoveValue(value);
                        }
                }

            }
            else
            {
                manageUnits.CreateBuild(pos);
            }
        }
        else { Debug.Log("you need create on ground!!!"); }
       
    }

    public void DeleteUnit()
    {
        
        if (CheckRemoveUnit(manageUnits._selected.Count))
        {
            selected.AddValue(manageUnits._selected.Count);
            manageUnits.KillSelectedUnits();
        }
    }

    private bool CheckAddUnit(int value)
    {
            if (selected != null)
            {
                if (selected._value - value >= 0)
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }
            
    }

    private bool CheckRemoveUnit(int value)
    {
        if(selected != null)
        {
            if (selected._value + value <= selected._limit)
            {
                return true;
            }
            else { return false; }
        }
        else
        {
            return false;
        }

    }

}
