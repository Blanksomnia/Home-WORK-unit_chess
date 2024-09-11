using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;

using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEngine.UI.CanvasScaler;

public class BattleField : MonoBehaviour
{
    public List<Cell> Cells = new List<Cell>();
    public List<Unit> Units = new List<Unit>();
    public List<Transform> Border = new List<Transform>();
    public GameObject _canvas;
    public void TeamQueue()
    {
        foreach (Cell _cell in Cells)
        {
            _cell._choice = "Lock";
            _cell.ResetSelect();
        }
        foreach (Unit _unit in Units)
        {
            _unit.BecomeLady();
            if (_unit.Team == _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text)
            {
                _unit.Cell._choice = "select";
            }
        }
        

    }

    private void Start()
    {
        GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();
        _canvas = gameObject;
        foreach (GameObject _unit in ObjectsFound)
        {
            if (_unit.transform.name == "Units")
            {
                foreach (Transform _unit2 in _unit.GetComponentInParent<Transform>())
                {
                    if (_unit2.GetComponent<Unit>())
                    {
                        Units.Add(_unit2.GetComponent<Unit>());
                    }
                }

            }



        }
        foreach (GameObject _cell in ObjectsFound)
        {
            if (_cell.transform.name == "Floors")
            {
                foreach (Transform _cell2 in _cell.GetComponentInParent<Transform>())
                {
                    if (_cell2.GetComponent<Cell>())
                    {
                        foreach (Unit _unit in Units)
                        {
                            if (_cell2.position == _unit.transform.position - new Vector3(0, 0.678f, 0))
                            {
                                _cell2.GetComponent<Cell>().Unit = _unit;
                                _unit.GetComponent<Unit>().Cell = _cell2.GetComponent<Cell>();

                            }

                        }
                        Cells.Add(_cell2.GetComponent<Cell>());
                    }
                }
            }

        }
        foreach (GameObject _border in ObjectsFound)
        {
            if (_border.transform.name == "borders")
            {
                foreach (Transform _border2 in _border.GetComponentInParent<Transform>())
                {
                    if (_border2.GetComponent<border>())
                    {
                        Border.Add(_border2);
                    }
                }

            }



        }
        TeamQueue();
    }

    public void OnPointerClickEvent(Cell cell, Unit UNIT)
    {
        List<Cell> CellSelect = new List<Cell>();
        var NeighbourType = new Neightbour().NeighbourType;
        if (UNIT.Lady != true)
        {
            CellSelect.Add(cell);

            foreach (Cell _cell in Cells)
            {

                _cell.GetComponent<Cell>()._choice = "Lock";
                for (int i = 0; i < NeighbourType.Count; i++)
                {
                   
                    if (_cell.transform.position == cell.transform.position + NeighbourType[i])
                    {
                        CellSelect.Add(_cell);                     
                    }
                    else
                    {
                        _cell.ResetSelect();
                    }                

                }

                   
                


            }
       
                for (int i = 0; i < CellSelect.Count; i++)
                {

                    if (i == 0)
                    {
                        CellSelect[i]._material = Resources.Load("Material/focus_material", typeof(Material)) as Material;

                        CellSelect[i].SetSelect();
                    }
                    else if ((CellSelect[i].transform.position == cell.transform.position + NeighbourType[2] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[7]) && CellSelect[i].Unit == null && UNIT.Team == "Red")
                    {

                        CellSelect[i]._choice = "ismove";
                        CellSelect[i].Unitselected = cell.Unit;
                        CellSelect[i]._material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                        CellSelect[i].SetSelect();

                    }
                    else if ((CellSelect[i].transform.position == cell.transform.position + NeighbourType[1] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[3]) && CellSelect[i].Unit == null && UNIT.Team == "Blue")
                    {
                        CellSelect[i]._choice = "ismove";
                        CellSelect[i].Unitselected = cell.Unit;
                        CellSelect[i]._material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                        CellSelect[i].SetSelect();
                    }

                    if(CellSelect[i].Unit != null)
                    {
                       if (CellSelect[i].Unit.Team != UNIT.Team)
                       {
                           var posit = cell.transform.position - CellSelect[i].transform.position;
                           var pos = cell.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                           bool CanKill = true;
                           foreach(Cell _cell in Cells)
                           {
                              if(_cell.transform.position == pos && _cell.Unit != null) { CanKill = false; }
                           }

                           foreach(Transform border in Border)
                           {
                               if (border.position == pos) { CanKill = false; }
                           }

                           if (CanKill == true)
                           {
                               CellSelect[i]._choice = "isattack";
                               CellSelect[i].Unitselected = cell.Unit;
                               CellSelect[i]._material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                               CellSelect[i].SetSelect();
                           }
                          
                       }
                    }     
                    
                                 



                }
                

            




        }
        if(UNIT.Lady == true)
        {
            CellSelect.Add(cell);
            for (int i = 0; i < 8; i++)
            {
                foreach (Cell _cell in Cells)
                {
                    _cell.GetComponent<Cell>()._choice = "Lock";
                    _cell.ResetSelect();

                    if(_cell.transform.position == cell.transform.position + new Vector3(i, 0, i))
                    {
                        CellSelect.Add(_cell);
                    }
                    if(_cell.transform.position == cell.transform.position + new Vector3(-i, 0, -i))
                    {
                        CellSelect.Add(_cell);
                    }
                    if (_cell.transform.position == cell.transform.position + new Vector3(i, 0, -i))
                    {
                        CellSelect.Add(_cell);
                    }
                    if (_cell.transform.position == cell.transform.position + new Vector3(-i, 0, i))
                    {
                        CellSelect.Add(_cell);
                    }
                }
            }
    

            for (int i = 0; i < CellSelect.Count; i++)
            {

                if (i == 0)
                {
                    CellSelect[i]._material = Resources.Load("Material/focus_material", typeof(Material)) as Material;

                    CellSelect[i].SetSelect();
                }
                else if(CellSelect[i].Unit == null && CellSelect[i].CanMove == true)
                {

                    CellSelect[i]._choice = "ismove";
                    CellSelect[i].Unitselected = cell.Unit;
                    CellSelect[i]._material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                    CellSelect[i].SetSelect();

                }


                if (CellSelect[i].Unit != null)
                {
                    if (CellSelect[i].Unit.Team != UNIT.Team)
                    {
                        var posit = cell.transform.position - CellSelect[i].transform.position;
                        var pos = new Vector3();
                        bool CanKill = true;
                        if (posit.x >= 1 && posit.z >= 1)
                        {
                            pos = CellSelect[i].transform.position + new Vector3(-1, 0, -1);
                        }
                        if (posit.x <= -1 && posit.z <= -1)
                        {
                            pos = CellSelect[i].transform.position + new Vector3(1, 0, 1);
                        }
                        if (posit.x >= 1 && posit.z <= -1)
                        {
                            pos = CellSelect[i].transform.position + new Vector3(-1, 0, 1);
                        }
                        if (posit.x <= -1 && posit.z >= 1)
                        {
                            pos = CellSelect[i].transform.position + new Vector3(1, 0, -1);
                        }
                        foreach (Cell _cell in Cells)
                        {
                            if (_cell.transform.position == pos && _cell.Unit != null) { CanKill = false; }
                            for (int c = 0; c < 8; c++)
                            {
                                if (posit.x >= 1 && posit.z >= 1)
                                {
                                    if (_cell.transform.position == CellSelect[i].transform.position + new Vector3(-c, 0, -c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                                if (posit.x <= -1 && posit.z <= -1)
                                {
                                    if (_cell.transform.position == CellSelect[i].transform.position + new Vector3(c, 0, c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                                if (posit.x >= 1 && posit.z <= -1)
                                {
                                    if (_cell.transform.position == CellSelect[i].transform.position + new Vector3(-c, 0, c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                                if (posit.x <= -1 && posit.z >= 1)
                                {
                                    if (_cell.transform.position == CellSelect[i].transform.position + new Vector3(c, 0, -c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                            }


                        }

                        foreach (Transform border in Border)
                        {
                            if (border.position == pos) { CanKill = false; }
                        }

                        if (CanKill == true)
                        {
                            CellSelect[i]._choice = "isattack";
                            CellSelect[i].Unitselected = cell.Unit;
                            CellSelect[i]._material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                            CellSelect[i].SetSelect();
                        }

                    }
                }



            }
        }
        
    }
    public void f(Cell cell, Unit UNIT, string f)
    {
        if (f == "ismove")
        {
            _canvas.GetComponent<BattleController>().CELL = cell;
            _canvas.GetComponent<BattleController>().UNIT = UNIT;
            _canvas.GetComponent<BattleController>().conf = true;
        }
        if (f == "isattack")
        {
            _canvas.GetComponent<BattleController>().CELL = cell;
            _canvas.GetComponent<BattleController>().UNIT = UNIT;
            _canvas.GetComponent<BattleController>().conf = false;
        }
    }

    public void IsMoved(Cell cell, Unit UNIT)
    {
        if (UNIT.Lady == false || UNIT.Lady == true)
        {
                cell.Unitselected = null;
                if (cell.Unit == null)
                {
                    if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Red")
                    {
                        _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Blue";
                    }
                    else if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Blue")
                    {
                        _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Red";
                    }
                    var pos = new Vector3(cell.transform.position.x, UNIT.transform.position.y, cell.transform.position.z);
                    UNIT.Move(pos);
                    UNIT.Cell.Unit = null;
                    cell.Unit = UNIT;
                    UNIT.Cell = cell;
                    

                }
        }
        TeamQueue();
    }
    public void IsAttack(Cell cell, Unit UNIT)
    {
        if (UNIT.Lady == false || UNIT.Lady == true)
            {
                if (cell.Unit != null)
                {

                    if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Red")
                    {
                        _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Blue";
                    }
                    else if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Blue")
                    {
                        _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Red";
                    }

                    var posit = UNIT.transform.position - (cell.transform.position + new Vector3(0, 0.678f, 0));
                    var pos = UNIT.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                    if(UNIT.Lady == true)
                    {
                    if (posit.x >= 1 && posit.z >= 1)
                    {
                        pos = cell.transform.position + new Vector3(-1, 0.678f, -1);
                    }
                    if (posit.x <= -1 && posit.z <= -1)
                    {
                        pos = cell.transform.position + new Vector3(1, 0.678f, 1);
                    }
                    if (posit.x >= 1 && posit.z <= -1)
                    {
                        pos = cell.transform.position + new Vector3(-1, 0.678f, 1);
                    }
                    if (posit.x <= -1 && posit.z >= 1)
                    {
                        pos = cell.transform.position + new Vector3(1, 0.678f, -1);
                    }
                    }
                    

                    UNIT.Move(pos);
                    UNIT.Cell.Unit = null;
                    cell.Unit.transform.position = new Vector3(0, 0, 0);
                    cell.Unit = null;
                foreach (Cell _cell in Cells)
                {
                    if (_cell.transform.position == pos - new Vector3(0, 0.678f, 0))
                    {                       
                        UNIT.Cell = _cell;
                        _cell.Unit = UNIT;
                    }
                }


            }
            }
        TeamQueue();
    }
    

}
