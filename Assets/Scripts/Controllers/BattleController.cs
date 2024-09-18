using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BattleController : MonoBehaviour
{
    PlayerInput _playerInput;

    InputAction _Restart;
    InputAction _confirm;
    InputAction _cancel;

    public bool canCancel = true;
    public bool conf;

    public Unit UNIT;
    public Cell CELL;

    GameObject Return1;
    GameObject Return2;

    UnityEngine.UI.Image ReturnScale;

    [SerializeField]
    BattleField _battleField;

    void Update()
    {
        if (_Restart.IsPressed())
        {
            Return1.SetActive(true);
            Return2.SetActive(true);
            ReturnScale.fillAmount += 0.001f;
            if (ReturnScale.fillAmount == 1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (_Restart.WasReleasedThisFrame())
        {
            Return1.SetActive(false);
            Return2.SetActive(false);
            ReturnScale.fillAmount = 0f;
        }
        if (_cancel.WasPressedThisFrame() && canCancel == true)
        {      
            foreach (Cell _cell in _battleField.Cells)
            {
                _cell.ResetSelect();
                _cell._choice = "Lock";
            }

            _battleField.TeamQueue();
        }

        if (_confirm.WasPressedThisFrame() && conf == true && CELL != null && UNIT != null)
        {
            _battleField.IsMoved(CELL, UNIT);
            CELL = null;
            UNIT = null;
        }
        if(_confirm.WasPressedThisFrame() && conf == false && CELL != null && UNIT != null)
        {
           _battleField.IsAttack(CELL, UNIT);
            CELL = null;
            UNIT = null;
        }

    }
  
    void Start()
    {
        Return1 = gameObject.transform.GetChild(0).gameObject;
        Return1.SetActive(false);

        Return2 = gameObject.transform.GetChild(1).gameObject;
        Return2.SetActive(false);
        ReturnScale = Return2.gameObject.transform.GetComponent<UnityEngine.UI.Image>();

        _playerInput = gameObject.transform.GetComponent<PlayerInput>();

        _Restart = _playerInput.actions["Tab"];
        _cancel = _playerInput.actions["Cancel"];
        _confirm = _playerInput.actions["Confirm"];
       
    }

    
}
