using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using Zenject;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] LayerMask player;
    LayerMask empty;

    InventoryUnits inventory;
    ManagerUnits manager;
    InputActionAsset input;

    InputAction selectUnit;
    InputAction selectClear;
    InputAction selectUnitGroup;
    InputAction moveTo;
    InputAction deleteUnit;
    InputAction addUnit;

    IStateUnitBehaviour character;

    [Inject]
    public void Construct(InputActionAsset inputs, InventoryUnits invent, ManagerUnits manage)
    {
        input = inputs;
        inventory = invent;
        manager = manage;
    }

    private void Awake()
    {
        string map = "ManageUnits";
        selectUnit = input.FindActionMap(map).FindAction("SelectUnit");
        selectUnitGroup = input.FindActionMap(map).FindAction("SelectGroupUnits");
        moveTo = input.FindActionMap(map).FindAction("MoveTo");
        deleteUnit = input.FindActionMap(map).FindAction("DeleteUnit");
        addUnit = input.FindActionMap(map).FindAction("AddUnit");
        selectClear = input.FindActionMap(map).FindAction("ClearSelected");
        selectUnit.Enable();
        selectUnitGroup.Enable();
        moveTo.Enable();
        deleteUnit.Enable();
        addUnit.Enable();
        selectClear.Enable();
    }

    private void Update()
    {
        if(selectUnit.WasPressedThisFrame())
        {
            if(character != null)
            {
                manager.SelectUnit(character);
            }
        }

        if (selectUnitGroup.WasPressedThisFrame())
        {
            manager.SelectGroup();
        }

        if(moveTo.WasPressedThisFrame())
        {
            manager.UnitMoveTo();
        }

        if (addUnit.WasPressedThisFrame())
        {
            inventory.AddUnit(1);
        }

        if (deleteUnit.WasPressedThisFrame())
        {
            inventory.DeleteUnit();
        }

        if (selectClear.WasPressedThisFrame())
        {
            manager.ClearSelected();
        }

        MouseOnPlayer();
        

    }

    private void MouseOnPlayer()
    {
        Vector3 pos = manager.MousePoint(player, empty);

        if(pos != Vector3.zero)
        {
            IStateUnitBehaviour NearChar = null;

            for (int i = 0; i < manager._activities.Count; i++)
            {
                if (i != 2)
                {
                    for (int j = 0; j < manager._activities[i].Count; j++)
                    {

                        if(NearChar == null)
                        {
                            NearChar = manager._activities[i][j];
                        }

                        if (Vector3.Distance(NearChar._transform().position, pos) > Vector3.Distance(manager._activities[i][j]._transform().position, pos))
                        {
                            NearChar = manager._activities[i][j];
                        }
                    }
                }

            }

            if(character != null)
            {
                if (character != NearChar)
                {
                    character.OnPointerExit();
                    NearChar.OnPointerEnter();
                    character = NearChar;
                }
                else
                {

                }
                    
            }
            else
            {
                character = NearChar;
                character.OnPointerEnter();
            }


            
        }
        else
        {
            if(character != null) { character.OnPointerExit(); character = null; }
        }

    }

}
