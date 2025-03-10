using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class MovingGroupAgent : ObjectsMover
{
    public Material enter;
    public Material select;
    public Material exit;

    Vector3 point;
    bool isMove = false;
    [SerializeField] float maxDistanceToPoint = 1f;
    [SerializeField] float maxDistanceToUnit = 1f;
    List<Unit> stopedUnits = new List<Unit>();
    List<Unit> movedUnits = new List<Unit>();

    public void UnitsMove(Vector3 vec)
    {
        point = vec;
        isMove = true;
        UpdateList();
    }

    public override void UpdateUnits()
    {
        UpdateList();
    }


    private void UpdateList()
    {

        movedUnits.Clear();
        stopedUnits.Clear();
        for (int i = 0; i < _units.Count; i++)
        {
            movedUnits.Add(_units[i]);

            if (isMove)
            {
                _units[i].navMeshAgent.SetDestination(point);
                _units[i].navMeshAgent.isStopped = false;
            }

        }
    }

    private void Update()
    {
        if (isMove)
        {
            if(UnitsOnPoint() == true)
            {
                isMove = false;
            }

        }


    }


    private bool UnitsOnPoint()
    {
        List<Unit> units = new List<Unit> ();
        if (stopedUnits.Count == 0)
        {
            for (int i = 0; i < movedUnits.Count; i++) 
            {

                if (Vector3.Distance(movedUnits[i].transform.position, point) <= maxDistanceToPoint)
                {
                    units.Add(movedUnits[i]);
                }
            }
        }
        else if(stopedUnits.Count > 0)
        {
            
            for(int i = 0; i < movedUnits.Count; i++)
            {
                for(int j = 0; j < stopedUnits.Count; j++)
                {

                    if (Vector3.Distance(stopedUnits[j].transform.position, movedUnits[i].transform.position) <= maxDistanceToUnit)
                    {
                        units.Add(movedUnits[i]);
                    }
                }
            }
        }

        for(int i = 0;i < units.Count; i++)
        {
            MoveStop(units[i]);          
        }

        if (movedUnits.Count == 0)
        {
            return true;
        }
        else { return false; }
    }

  

    private void MoveStop(Unit un)
    {
        un.navMeshAgent.isStopped = true;
        stopedUnits.Add(un);
        movedUnits.Remove(un);
    }

}
