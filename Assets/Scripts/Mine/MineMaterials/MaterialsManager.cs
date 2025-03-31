using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsManager : MonoBehaviour
{
    [SerializeField] private List<MaterialMine> materials;
    [SerializeField] GUIMaterials mat;
    public List<MaterialMine> _materials => materials;


    public MaterialMine material(TypeMine type)
    {
        MaterialMine mat = null;
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i]._type == type)
            {
                mat = materials[i];
            }
        }

        return mat;
    }

    public bool CheckAdd(TypeMine type, int value)
    {
        MaterialMine mat = material(type);

        if(mat == null)
        {
            return false;
        }
        else
        {
            if(mat._value + value <= mat._limit)
            {
                return true;
            }
            else { return false; }
        }
    }

    public bool CheckRemove(TypeMine type, int value)
    {
        MaterialMine mat = material(type);

        if (mat == null)
        {
            return false;
        }
        else
        {
            if (mat._value - value >= 0)
            {
                return true;
            }
            else { return false; }
        }
    }

    public void AddValue(TypeMine type, int value)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i]._type == type)
            {
                materials[i].Add(value);
            }
        }
        mat.UpdMaterlials();
    }

    public void RemoveValue(TypeMine type, int value)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i]._type == type)
            {
               materials[i].Remove(value);

            }
        }
        mat.UpdMaterlials();
    }

}
