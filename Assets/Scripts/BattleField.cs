using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class BattleField
{

    public List<GameObject> Cells = new List<GameObject>();
    public List<GameObject> Units = new List<GameObject>();
    List<GameObject> Border = new List<GameObject>();
    List<GameObject> CellSelect = new List<GameObject>();
    List<GameObject> Cellkilled = new List<GameObject>();
    public GameObject canvas;
    public void TeamQueue()
    {
        FoundUnitsCells();
        foreach (GameObject _cell in Cells)
        {
            if (_cell.GetComponent<Cell>().Team == canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text)
            {
                _cell.GetComponent<Cell>()._choice = "select";
            }
            else
            {
                _cell.GetComponent<Cell>()._choice = "Lock";
            }
        }

    }

    public void FoundUnitsCells()
    {

        GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();

        foreach (GameObject _unit in ObjectsFound)
        {

            if (_unit.transform.name == "Canvas")
            {
                canvas = _unit;
            }
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
                        _cell.GetComponent<Cell>().Team = _unit.GetComponent<Unit>().Team;
                        _unit.GetComponent<Unit>().Cell = _cell;

                    }

                }
                Cells.Add(_cell);
            }
        }
        foreach (GameObject _border in ObjectsFound)
        {

            if (_border.transform.GetComponent<border>())
            {
                Border.Add(_border);
            }


        }

    }
    public void OnPointerClickEvent(GameObject cell)
    {
        FoundUnitsCells();
        Material material;
        var NeighbourType = new Neightbour().NeighbourType;

        if (cell.GetComponent<Cell>().Unit != null && cell.GetComponent<Cell>().Unit.GetComponent<Unit>().Lady != true)
        {
            CellSelect.Add(cell);
            var unit = cell.GetComponent<Cell>().Unit;
            unit.GetComponent<Unit>().IsSelected = true;
            foreach (GameObject _cell in Cells)
            {

                _cell.GetComponent<Cell>()._choice = "Lock";
                for (int i = 0; i < NeighbourType.Count; i++)
                {


                    if (_cell.transform.position == cell.transform.position + NeighbourType[i])
                    {
                        CellSelect.Add(_cell);
                    }
                }

                for (int i = 0; i < CellSelect.Count; i++)
                {

                    if (i == 0)
                    {
                        material = Resources.Load("Material/focus_material", typeof(Material)) as Material;

                        CellSelect[i].transform.GetComponent<Cell>().SetSelect(material);
                    }
                    else if (CellSelect[i].GetComponent<Cell>().Unit == null && cell.GetComponent<Cell>().Team == "Red" && CellSelect[i].transform.position == cell.transform.position + NeighbourType[2] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[7])
                    {

                        CellSelect[i].transform.GetComponent<Cell>()._choice = "ismove";
                        material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                        CellSelect[i].transform.GetComponent<Cell>().SetSelect(material);

                    }
                    else if (CellSelect[i].GetComponent<Cell>().Unit == null && cell.GetComponent<Cell>().Team == "Blue" && CellSelect[i].transform.position == cell.transform.position + NeighbourType[1] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[3])
                    {
                        CellSelect[i].transform.GetComponent<Cell>()._choice = "ismove";
                        material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                        CellSelect[i].transform.GetComponent<Cell>().SetSelect(material);
                    }

                        

                         bool cankill = true;
                        if (CellSelect[i].GetComponent<Cell>().Unit != null && unit.GetComponent<Unit>().Team != CellSelect[i].GetComponent<Cell>().Unit.GetComponent<Unit>().Team )
                        {
                            var posit = cell.transform.position - CellSelect[i].transform.position;
                            var pos = cell.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                            foreach (GameObject _cell2 in Cells)
                            {
                               
                                if (_cell2.transform.position == pos)
                                {
                                    if(_cell2.GetComponent<Cell>().Unit != null)
                                    {
                                        cankill = false; 
                                    }
                                    

                                }



                        }
                            foreach (GameObject _border in Border)
                        {
                            if (_border.transform.position == pos)
                            {
                                cankill = false;


                            }
                        }

                        if(cankill == true)
                        {
                            CellSelect[i].transform.GetComponent<Cell>()._choice = "isattack";
                            material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                            _cell.transform.GetComponent<Cell>().SetSelect(material);
                            
                        }
                        

                        }
                    if (_cell != CellSelect[i])
                    {
                        _cell.transform.GetComponent<Cell>().ResetSelect();
                    }


                }

            }





        }
        if (cell.GetComponent<Cell>().Unit != null && cell.GetComponent<Cell>().Unit.GetComponent<Unit>().Lady == true)
        {
            var unit = cell.GetComponent<Cell>().Unit;
            unit.GetComponent<Unit>().IsSelected = true;
            CellSelect.Add(cell);
            foreach (GameObject _cell in Cells)
            {
                for(int i = 0; i < 8; i++)
                {
                    if (_cell.transform.position == cell.transform.position + new Vector3(0, 0, i))
                    {
                        CellSelect.Add(_cell);
                    }
                    if (_cell.transform.position == cell.transform.position + new Vector3(i, 0, 0))
                    {
                        CellSelect.Add(_cell);
                    }
                    if (_cell.transform.position == cell.transform.position + new Vector3(0, 0, -i))
                    {
                        CellSelect.Add(_cell);
                    }
                    if (_cell.transform.position == cell.transform.position + new Vector3(-i, 0, 0))
                    {
                        CellSelect.Add(_cell);
                    }
                }
                for(int i = 0;i < CellSelect.Count;i++)
                {
  
                    if (i == 0)
                    {
                        material = Resources.Load("Material/focus_material", typeof(Material)) as Material;

                        CellSelect[i].transform.GetComponent<Cell>().SetSelect(material);
                    }
                    else if(CellSelect[i].GetComponent<Cell>().CanMove == true)
                    {
                        
                        CellSelect[i].transform.GetComponent<Cell>()._choice = "ismove";
                        material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                        CellSelect[i].transform.GetComponent<Cell>().SetSelect(material);


                        bool cankill = true;
                        if (CellSelect[i].GetComponent<Cell>().Unit != null && unit.GetComponent<Unit>().Team != CellSelect[i].GetComponent<Cell>().Unit.GetComponent<Unit>().Team)
                        {
                            var posit = cell.transform.position - CellSelect[i].transform.position;

                            var pos = new Vector3();
                            if (posit.x != 0)
                            {
                                posit.x = 1;
                                for (int c = -8; c < 8; c++)
                                {
                                    pos = cell.transform.position + new Vector3(-c, 0, cell.transform.position.z);
                                }
                            }
                            if (posit.z != 0)
                            {
                                posit.z = 1;
                                for (int c = -8; c < 8; c++)
                                {
                                    pos = cell.transform.position + new Vector3(cell.transform.position.x * 2, 0, -c);
                                }
                            }


                            foreach (GameObject _cell2 in Cells)
                            {

                                if (_cell2.transform.position == pos)
                                {
                                    if (_cell2.GetComponent<Cell>().Unit != null)
                                    {
                                        cankill = false;
                                    }


                                }



                            }
                            foreach (GameObject _border in Border)
                            {
                                if (_border.transform.position == pos)
                                {
                                    cankill = false;


                                }
                            }

                            if (cankill == true)
                            {
                                material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                                CellSelect[i].transform.GetComponent<Cell>().SetSelect(material);

                            }


                        }
                        if (_cell != CellSelect[i])
                        {
                            _cell.transform.GetComponent<Cell>().ResetSelect();
                        }
                    }
                    if (_cell != CellSelect[i])
                    {
                        _cell.transform.GetComponent<Cell>().ResetSelect();
                    }

                }

            }
        }


    }

    public void IsMoved(GameObject cell)
    {
        FoundUnitsCells();
        foreach (GameObject _unit in Units)
        {

            _unit.GetComponent<Unit>().Cell = null;

            if (_unit.GetComponent<Unit>().IsSelected == true && _unit.GetComponent<Unit>().Lady == false)
            {
                _unit.GetComponent<Unit>().IsSelected = false;

                if (cell.GetComponent<Cell>().Unit == null)
                {
                    if (canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text == "Red")
                    {
                        canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Blue";
                    }
                    else if (canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text == "Blue")
                    {
                        canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Red";
                    }
                    var pos = new Vector3(cell.transform.position.x, _unit.transform.position.y, cell.transform.position.z);
                    
                    _unit.GetComponent<Unit>().Move(pos);
                    foreach (GameObject _cell in Cells)
                    {
                        _cell.GetComponent<Cell>().ResetSelect();
                        _cell.GetComponent<Cell>()._choice = "select";
                        _cell.GetComponent<Cell>().Unit = null;

                    }

                }
            }
            if (_unit.GetComponent<Unit>().IsSelected == true && _unit.GetComponent<Unit>().Lady == true)
            {
                _unit.GetComponent<Unit>().IsSelected = false;
                Debug.Log("f");
                if (cell.GetComponent<Cell>().Unit == null)
                {
                    Debug.Log("f");
                    if (canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text == "Red")
                    {
                        canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Blue";
                    }
                    else if (canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text == "Blue")
                    {
                        canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Red";
                    }
                    var posit = _unit.transform.position - (cell.transform.position + new Vector3(0, 0.678f, 0));

                    var pos = new Vector3();
                    if (posit.x != 0)
                    {
                        posit.x = 1;
                        for (int c = -8; c < 8; c++)
                        {
                            pos = _unit.transform.position + new Vector3(-c, 0, cell.transform.position.z);
                            foreach (GameObject _unit1 in Units)
                            {
                                if (_unit1.transform.position == pos)
                                {
                                    _unit1.transform.position = new Vector3(0, 0, 0);
                                }
                            }
                        }
                    }
                    if (posit.z != 0)
                    {
                        posit.z = 1;
                        for (int c = -8; c < 8; c++)
                        {
                            pos = _unit.transform.position + new Vector3(cell.transform.position.x, 0, -c);
                            foreach (GameObject _unit1 in Units)
                            {
                                if (_unit1.transform.position == pos)
                                {
                                    _unit1.transform.position = new Vector3(0, 0, 0);
                                }
                            }
                        }

                    }

                    _unit.GetComponent<Unit>().Move(cell.transform.position + new Vector3(0, 0.678f, 0));
                    foreach (GameObject _cell in Cells)
                    {
                        _cell.GetComponent<Cell>().ResetSelect();
                        _cell.GetComponent<Cell>()._choice = "select";
                        _cell.GetComponent<Cell>().Unit = null;

                    }

                }
            }
        }

        TeamQueue();


    }
    public void IsAttack(GameObject cell)
    {
       
        FoundUnitsCells();
        foreach (GameObject _unit in Units)
        {

            _unit.GetComponent<Unit>().Cell = null;
            if (_unit.GetComponent<Unit>().IsSelected == true && _unit.GetComponent<Unit>().Lady == false)
            {
                _unit.GetComponent<Unit>().IsSelected = false;
                if (cell.GetComponent<Cell>().Unit != null)
                {

                    if (canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text == "Red")
                    {
                        canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Blue";
                    }
                    else if (canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text == "Blue")
                    {
                        canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<TextMeshProUGUI>().text = "Red";
                    }
                    var posit = _unit.transform.position - (cell.transform.position + new Vector3(0, 0.678f, 0));
                    var pos = _unit.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                    


                    _unit.GetComponent<Unit>().Move(pos);

                    cell.GetComponent<Cell>().Unit.transform.position = new Vector3(0, 0, 0);
                    foreach (GameObject _cell in Cells)
                    {
                        _cell.GetComponent<Cell>().ResetSelect();
                        _cell.GetComponent<Cell>()._choice = "select";
                        _cell.GetComponent<Cell>().Unit = null;

                    }

                }
            }
    
        }

        TeamQueue();
    }
    

}
