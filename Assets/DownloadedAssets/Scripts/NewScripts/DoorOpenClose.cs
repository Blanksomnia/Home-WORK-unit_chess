using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    bool isOpen = false;
    bool IsInteract = false;
    Quaternion RotateClosed;
    Quaternion RotateOpen;

    private void Awake()
    {
        RotateClosed = Quaternion.Euler(new Vector3(0, 0, 0));
        RotateOpen = Quaternion.Euler(new Vector3(0, 87, 0));

    }

    private void Update()
    {
        if (IsInteract)
        {
            if(!isOpen)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, RotateOpen, 1);
                if(transform.rotation.y >= RotateOpen.y)
                {
                    isOpen = true;
                    IsInteract = false;
                }
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, RotateClosed, 1);
                if (transform.rotation.y <= RotateClosed.y)
                {
                    isOpen = false;
                    IsInteract = false;
                }
            }
        }
    }

    public void Activate()
    {
        IsInteract = true;
    }

}
