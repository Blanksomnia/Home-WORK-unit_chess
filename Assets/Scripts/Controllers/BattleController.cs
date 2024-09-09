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

    public List<GameObject> Cells = new BattleField().Cells;
    public List<GameObject> Units = new BattleField().Units;

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
        if (_cancel.WasPressedThisFrame())
        {
            GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (GameObject _unit in ObjectsFound)
            {
               
                if (_unit.transform.GetComponent<Unit>())
                {
                    Units.Add(_unit);
                }


            }
            foreach (GameObject _cell in ObjectsFound)
            {

                if (_cell.transform.GetComponent<Cell>())
                {
                    foreach (GameObject _unit in Units)
                    {
                        if (_cell.transform.position == _unit.transform.position - new Vector3(0, 0.678f, 0))
                        {
                            _cell.GetComponent<Cell>().Unit = _unit;
                            _unit.GetComponent<Unit>().Cell = _cell;

                        }

                    }
                    Cells.Add(_cell);
                }
            }
            foreach (GameObject _cell in Cells)
            {
                _cell.GetComponent<Cell>()._choice = "Lock";
                foreach (GameObject _unit in Units)
                {
                    if (_unit.GetComponent<Unit>().Cell != null)
                    {
                        _unit.GetComponent<Unit>().IsSelected = false;
                    }
                    
                }
                
                _cell.GetComponent<Cell>().ResetSelect();
                
            }
            new BattleField().TeamQueue();
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
        new BattleField().TeamQueue();
        _playerInput = gameObject.transform.GetComponent<PlayerInput>();

        _Restart = _playerInput.actions["Tab"];
        _cancel = _playerInput.actions["Cancel"];



    }

    
}
