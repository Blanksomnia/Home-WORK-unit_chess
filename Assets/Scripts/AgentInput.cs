using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgentInput : MonoBehaviour
{
    [SerializeField] MovingGroupAgent movingGroupAgent;

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            movingGroupAgent.addUnit(PosMouse());
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            movingGroupAgent.UnitsMove(PosMouse());
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (movingGroupAgent.selected)
            {
                movingGroupAgent.selected.ChangeMat(movingGroupAgent.exit);
                movingGroupAgent.KillUnit(movingGroupAgent.selected);
                movingGroupAgent.selected = null;
            }

        }
    }

    private Vector3 PosMouse() 
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 VEC = ray.origin;
        VEC.y = transform.position.y + 1f;
        return VEC;
    }

}
