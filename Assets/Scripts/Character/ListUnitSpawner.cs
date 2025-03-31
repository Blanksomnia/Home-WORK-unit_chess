using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUnitSpawner : MonoBehaviour
{
    [SerializeField] List<UnitSpawner> units = new List<UnitSpawner>();
    
    public GameObject UnitCreate(TypeUnits type)
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i]._type == type)
            {
                return units[i]._unit;
            }
        }
        return null;
    }
}
