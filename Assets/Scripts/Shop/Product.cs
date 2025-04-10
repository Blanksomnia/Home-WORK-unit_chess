using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Zenject;

public class Product : MonoBehaviour
{
    [SerializeField] CellUnit cell;
    [SerializeField] ProductGUI gui;
    public CellUnit _cell => cell;
    [SerializeField] List<MaterialMine> cost;
    MaterialsManager materials;

    [Inject]
    public void Construct(MaterialsManager manMat)
    {
        materials = manMat;
    }

    private void Awake()
    {
        gui.GetName(cell._type);
        gui.GetCost(cost);
    }

    private bool Check()
    {
        if (cell._value + 1 <= cell._limit)
        {
            int satisfied = 0;

            for (int i = 0; i < materials._materials.Count; ++i)
            {
                for (int j = 0; j < cost.Count; ++j)
                {
                    if (materials._materials[i]._type == cost[j]._type)
                    {
                        if (materials._materials[i]._value >= cost[j]._value)
                        {
                            satisfied += 1;
                        }

                    }
                }
            }

            return satisfied == cost.Count;
        }
        else
        {
            print("cell is full");
            return false;
        }

    }

    public void Buy()
    {
        if (Check())
        {
            for (int i = 0; i < materials._materials.Count; ++i)
            {
                for (int j = 0; j < cost.Count; ++j)
                {
                    materials.RemoveValue(cost[j]._type, cost[j]._value);
                }
            }
            cell.AddValue(1);
            print("buy done");
        }
        else
        {
            print("not enought resources");
        }
    }

}

