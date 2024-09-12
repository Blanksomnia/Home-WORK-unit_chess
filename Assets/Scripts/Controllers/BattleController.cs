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
    public GameObject _canvas;
    public bool canCancel = true;
    public bool conf;
    public Unit UNIT;
    public Cell CELL;

    void Update()
    {
        if (_Restart.IsPressed())
        {
            _canvas.transform.GetChild(0).gameObject.SetActive(true);
            _canvas.transform.GetChild(1).gameObject.SetActive(true);
            _canvas.transform.GetChild(1).gameObject.transform.GetComponent<UnityEngine.UI.Image>().fillAmount += 0.001f;
            if (_canvas.transform.GetChild(1).gameObject.transform.GetComponent<UnityEngine.UI.Image>().fillAmount == 1f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (_Restart.WasReleasedThisFrame())
        {
            _canvas.transform.GetChild(0).gameObject.SetActive(false);
            _canvas.transform.GetChild(1).gameObject.SetActive(false);
            _canvas.transform.GetChild(1).gameObject.transform.GetComponent<UnityEngine.UI.Image>().fillAmount = 0f;
        }
        if (_cancel.WasPressedThisFrame() && canCancel == true)
        {      
            foreach (Cell _cell in _canvas.GetComponent<BattleField>().Cells)
            {
                _cell.ResetSelect();
                _cell._choice = "Lock";
            }
            _canvas.GetComponent<BattleField>().TeamQueue();
        }

        if (_confirm.WasPressedThisFrame() && conf == true && CELL != null && UNIT != null)
        {
            _canvas.GetComponent<BattleField>().IsMoved(CELL, UNIT);
            CELL = null;
            UNIT = null;
        }
        if(_confirm.WasPressedThisFrame() && conf == false && CELL != null && UNIT != null)
        {
            _canvas.GetComponent<BattleField>().IsAttack(CELL, UNIT);
            CELL = null;
            UNIT = null;
        }

    }
  
    void Start()
    {
        GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject _object in ObjectsFound)
        {
            if (_object.transform.name == "Canvas")
            {
                _canvas = _object;
                _object.transform.GetChild(0).gameObject.SetActive(false);
                _object.transform.GetChild(1).gameObject.SetActive(false);
            }

        }
        
        _playerInput = gameObject.transform.GetComponent<PlayerInput>();
        _Restart = _playerInput.actions["Tab"];
        _cancel = _playerInput.actions["Cancel"];
        _confirm = _playerInput.actions["Confirm"];
       

    }

    
}
