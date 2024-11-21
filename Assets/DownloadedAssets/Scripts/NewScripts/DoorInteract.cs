using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] Transform Parent;
    List<DoorOpenClose> Doors = new List<DoorOpenClose>();

    private void Awake()
    {
        foreach (DoorOpenClose t in Parent.GetComponentsInChildren<DoorOpenClose>())
        {
            Doors.Add(t);
        }

    }

    private void Update()
    {
        InteractInput();
    }

    private void InteractInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.forward * 5, 5);
            Debug.DrawRay(transform.position, transform.forward * 5, Color.blue);
            if (hit.Length > 0)
            {
                int st = 0;
                for(int j = 0; j < hit.Length; j++)
                {
                    float dist = Vector3.Distance(hit[j].collider.transform.position, transform.position);
                    float distSt = Vector3.Distance(hit[st].collider.transform.position, transform.position);
                    if(dist < distSt)
                    {
                        st = j;
                    }
                }

                for(int j = 0;j < Doors.Count; j++)
                {
                    if (hit[st].transform.parent.transform == Doors[j].transform)
                    {
                        Doors[j].Activate();
                    }
                }

                    
            }
        }
    }



}
