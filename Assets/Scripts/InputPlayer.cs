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
    [SerializeField] GameObject panel;
    LayerMask empty;

    InventoryUnits inventory;
    ManagerUnits manager;
    InputActionAsset input;

    InputAction selectUnit;
    InputAction moveUnits;


    IStateUnitBehaviour character = null;

    [Inject]
    public void Construct(InputActionAsset inputs, InventoryUnits invent, ManagerUnits manage)
    {
        input = inputs;
        inventory = invent;
        manager = manage;
    }

    private void Awake()
    {
        panel.gameObject.SetActive(false);

        string map = "ManageUnits";
        selectUnit = input.FindActionMap(map).FindAction("SelectUnit");
        moveUnits = input.FindActionMap(map).FindAction("MoveUnits");

        selectUnit.Enable();
        moveUnits.Enable();

    }

    private void Update()
    {
        MouseOnPlayer();

        if (moveUnits.WasPressedThisFrame())
        {
            if(character != null)
            {

            }
            else
            {
                manager.UnitMoveTo();
            }
        }

        if(selectUnit.WasPressedThisFrame())
        {
            if(character == null)
            {
                inventory.AddSelected(1);

            }
            else
            {
                manager.SelectUnit(character);
                CheckUnits();

            }

        }

    }

    private void CheckUnits()
    {
        if(manager._selected.Count > 0)
        {
            panel.gameObject.SetActive(true);
        }
        else
        {
            panel.gameObject.SetActive(false);
        }
    }

    public void SelectGroupButtom()
    {
        manager.SelectGroup();
        panel.gameObject.SetActive(false);
    }

    public void DeleteSelectsButtom()
    {
        inventory.DeleteUnit();
        panel.gameObject.SetActive(false);
    }

    public void ClearSelectsButtom()
    {
        manager.ClearSelected();
        panel.gameObject.SetActive(false);
    }

    private void MouseOnPlayer()
    {

        Vector3 pos = manager.MousePoint(player, empty);

        if(pos != Vector3.zero)
        {
            IStateUnitBehaviour NearChar = null;

            for (int i = 0; i < manager._activities().Count; i++)
            {
                if (i != 2)
                {
                    for (int j = 0; j < manager._activities()[i].Count; j++)
                    {

                        if(NearChar == null)
                        {
                            NearChar = manager._activities()[i][j];
                        }

                        if (Vector3.Distance(NearChar._transform().position, pos) > Vector3.Distance(manager._activities()[i][j]._transform().position, pos))
                        {
                            NearChar = manager._activities()[i][j];
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
