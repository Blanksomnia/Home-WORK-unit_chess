using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerBall : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] LogicPoints logic;
    [SerializeField] Transform MovementObject;
    Vector3 rotEnd;
    bool swap = false;

    private void Update()
    {

        PCInput();

        if (logic.canRotate && !logic.Activated)
        {
            RotateAnimation();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(logic.Activated && !swap)
        {
            rotEnd = MovementObject.eulerAngles;
            rotEnd.z += 90;
            logic.canMove = false;
            logic.canRotate = true;
            logic.Activated = false;
        }

        if (swap)
        {
            swap = false;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (logic.Activated)
        {
            swap = true;
            if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
            {

                if (eventData.delta.x > 0)
                {
                    logic.MoveStatus(logic.list.YOU.PRIGHT);
                }
                else
                {
                    logic.MoveStatus(logic.list.YOU.PLEFT);
                }


            }
            else
            {
                if (eventData.delta.y > 0)
                {
                    logic.MoveStatus(logic.list.YOU.PUP);
                }
                else
                {
                    logic.MoveStatus(logic.list.YOU.PDOWN);
                }


            }
        }
        

    }

    public void OnDrag(PointerEventData eventData)

    {

    }

    private void PCInput()
    {
        if (logic.Activated)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                logic.MoveStatus(logic.list.YOU.PUP);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                logic.MoveStatus(logic.list.YOU.PDOWN);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                logic.MoveStatus(logic.list.YOU.PRIGHT);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                logic.MoveStatus(logic.list.YOU.PLEFT);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && logic.Activated)
        {
            rotEnd = MovementObject.eulerAngles;
            rotEnd.z += 90;
            logic.canMove = false;
            logic.canRotate = true;
            logic.Activated = false;

        }

    }


    private void RotateAnimation()
    {

        quaternion p = MovementObject.rotation;
        MovementObject.rotation = Quaternion.Euler(Vector3.Lerp(MovementObject.eulerAngles, rotEnd, 11.0f * Time.deltaTime));

        if (p == MovementObject.rotation)
        {
            MovementObject.rotation = Quaternion.Euler(rotEnd);
            logic.UpdatePoints();
            logic.canMove = false;
            logic.canRotate = false;
            logic.Activated= true;
        }

    }
}
