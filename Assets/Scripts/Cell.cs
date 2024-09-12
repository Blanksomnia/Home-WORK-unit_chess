using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public string _choice = "select";
    public Unit Unit;
    public Unit Unitselected;
    public bool becomeLadyRed;
    public bool becomeLadyBlue;
    [SerializeField]
    public bool CanMove;
    public Material _material;
    public GameObject _canvas;
    public List<Vector3> NeighbourType = new Neightbour().NeighbourType;
    public List<Cell> CellSelect = new List<Cell>();
    public List<Cell> CellSelectLady = new List<Cell>();
    public List<Cell> MovedAfterAttack = new List<Cell>();
    public List<Cell> MovedAfterAttackLady = new List<Cell>();
    void Start()
    {
        if (CanMove == true)
        {
            GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject _object in ObjectsFound)
            {
                if (_object.transform.name == "Canvas")
                {
                    _canvas = _object;

                }

            }
            foreach (Cell _cell in _canvas.GetComponent<BattleField>().Cells)
            {
                for (int i = 0; i < NeighbourType.Count; i++)
                {

                    if (_cell.transform.position == gameObject.transform.position + NeighbourType[i])
                    {
                        CellSelect.Add(_cell);
                    }

                }

            }

            for (int i = 0; i < 8; i++)
            {
                foreach (Cell _cell in _canvas.GetComponent<BattleField>().Cells)
                {
                    if (_cell.transform.position == gameObject.transform.position + new Vector3(i, 0, i))
                    {
                        CellSelectLady.Add(_cell);
                    }
                    if (_cell.transform.position == gameObject.transform.position + new Vector3(-i, 0, -i))
                    {
                        CellSelectLady.Add(_cell);
                    }
                    if (_cell.transform.position == gameObject.transform.position + new Vector3(i, 0, -i))
                    {
                        CellSelectLady.Add(_cell);
                    }
                    if (_cell.transform.position == gameObject.transform.position + new Vector3(-i, 0, i))
                    {
                        CellSelectLady.Add(_cell);
                    }
                }
            }

            for (int i = 0; i < CellSelect.Count; i++)
            {
                var posit = gameObject.transform.position - CellSelect[i].transform.position;
                var pos = gameObject.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                foreach (Cell _cell in _canvas.GetComponent<BattleField>().Cells)
                {
                    if (_cell.transform.position == pos) { MovedAfterAttack.Add(_cell); }
                    
                }
                foreach (Cell _cell in _canvas.GetComponent<BattleField>().Border)
                {
                    if (_cell.transform.position == pos) { MovedAfterAttack.Add(_cell); }
                }
            }
            for(int i = 0;i < CellSelectLady.Count;i++)
            {
                var posit = gameObject.transform.position - CellSelectLady[i].transform.position;
                var pos = new Vector3();
                if (posit.x >= 1 && posit.z >= 1)
                {
                    pos = CellSelectLady[i].transform.position + new Vector3(-1, 0, -1);
                }
                if (posit.x <= -1 && posit.z <= -1)
                {
                    pos = CellSelectLady[i].transform.position + new Vector3(1, 0, 1);
                }
                if (posit.x >= 1 && posit.z <= -1)
                {
                    pos = CellSelectLady[i].transform.position + new Vector3(-1, 0, 1);
                }
                if (posit.x <= -1 && posit.z >= 1)
                {
                    pos = CellSelectLady[i].transform.position + new Vector3(1, 0, -1);
                }

                foreach (Cell _cell in _canvas.GetComponent<BattleField>().Cells)
                {
                    if(_cell.transform.position == pos) { MovedAfterAttackLady.Add(_cell); }
                }

                foreach (Cell _cell in _canvas.GetComponent<BattleField>().Border)
                {
                    if (_cell.transform.position == pos) { MovedAfterAttackLady.Add(_cell); }
                }
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(CanMove == true)
        gameObject.transform.Find("focus").GetComponent<MeshRenderer>().enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(CanMove == true)
        _canvas.GetComponent<PlayerController>().choice(gameObject.GetComponent<Cell>(), _choice, Unit, Unitselected);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(CanMove == true)
        gameObject.transform.Find("focus").GetComponent<MeshRenderer>().enabled = false;
    }
    public void SetSelect()
    {
        gameObject.transform.Find("select").GetComponent<MeshRenderer>().material = _material;
        gameObject.transform.Find("select").GetComponent<MeshRenderer>().enabled = true;
  
    }
    public void ResetSelect()
    {
        gameObject.transform.Find("select").GetComponent<MeshRenderer>().enabled = false;
    }

}
