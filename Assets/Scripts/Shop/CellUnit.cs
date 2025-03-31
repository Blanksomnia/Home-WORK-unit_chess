using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;
using static UnityEngine.UI.Button;

public class CellUnit : MonoBehaviour
{
    [SerializeField] private ButtonCell cell;


    InventoryUnits inventoryUnits;
    [SerializeField] private TypeUnits type;
    public  TypeUnits _type => type;
    [SerializeField] int value = 0;
    [SerializeField] int limit = 50;
    [HideInInspector] public int _value => value;
    [HideInInspector] public int _limit => limit;


    [Inject]
    public void Construct(InventoryUnits invent)
    {
        inventoryUnits = invent;
        UpdScore();
    }

    private void UpdScore()
    {
        cell.GetValue();
    }

    public void Click()
    {
        inventoryUnits.GetSelect(this);
    }

    public void RemoveValue(int v)
    {
        if(value - v >= 0)
        {
            value -= v;
        }
        else
        {
            value = 0;
        }
        UpdScore();
    }

    public void AddValue(int v)
    {
        if (value + v <= limit)
        {
            value += v;
        }
        else
        {
            value = limit;
        }
        UpdScore();
    }

}
