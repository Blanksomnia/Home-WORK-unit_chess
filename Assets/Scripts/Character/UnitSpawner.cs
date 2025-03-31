using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject unit;
    [SerializeField] TypeUnits type;

    public GameObject _unit => unit;
    public TypeUnits _type => type;
}
