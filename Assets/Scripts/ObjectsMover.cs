using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ObjectsMover : MonoBehaviour
{
    [SerializeField] GameObject unitPrefab;
    [SerializeField] int maxUnits = 40;

    List<Unit> unitsActivities = new List<Unit>();
    List<Unit> exUnits = new List<Unit>();

   [HideInInspector] public Unit selected;
   [HideInInspector] public List<Unit> _units => unitsActivities;

    private void Awake()
    {
        for(int i = 0; i < maxUnits; i++)
        {
            AddExUnit(Instantiate(unitPrefab, transform).GetComponent<Unit>());
        }
        
    }

    private void AddExUnit(Unit unit)
    {
        unit.transform.localPosition = Vector3.zero;

        if(unit.rb != null)
        unit.rb.isKinematic = true;

        if(unit.navMeshAgent != null)
        unit.navMeshAgent.enabled = false;

        exUnits.Add(unit);

    }

    public virtual void UpdateUnits() { }

    public void KillUnit(Unit unit)
    {
        for (int i = 0; i < unitsActivities.Count; i++)
        {
            if (unitsActivities[i] == unit)
            {
                unitsActivities.RemoveAt(i);
            }
        }
        AddExUnit(unit);
        Debug.Log("Unit Deleted!!!");
        Debug.Log("Units - " + unitsActivities.Count + " / MaxUnits - " + maxUnits);
        UpdateUnits();
    }

    public void addUnit(Vector3 pos)
    {
        if (exUnits.Count != 0)
        {
            exUnits[0].transform.position = pos;
            unitsActivities.Add(exUnits[0]);
            exUnits[0].rb.isKinematic = false;
            exUnits[0].navMeshAgent.enabled = true;
            exUnits.RemoveAt(0);
            Debug.Log("Unit Added!!!");
            Debug.Log("Units - " + unitsActivities.Count + " / MaxUnits - " + maxUnits);
        }
        else
        {
            Debug.Log("Units is full!!!");
        }
        UpdateUnits();
    }
}
