using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMine : MonoBehaviour
{
    [SerializeField] TypeMine type => _type;
    public TypeMine _type;
    [SerializeField] int value = 0;
    private int limitValue = 500;
    public int _value => value;
    public int _limit => limitValue;


    public void UpdMaterial()
    {

    }
    public void Add(int v)
    {
        value += v;
        if (value > limitValue)
        {
            value = limitValue;
        }
        UpdMaterial();
    }

    public void Remove(int v)
    {
        value -= v; 
        if (value < 0)
        {
            value = 0;
        }
        UpdMaterial();
    }
}
