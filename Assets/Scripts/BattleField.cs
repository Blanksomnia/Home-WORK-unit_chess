using JetBrains.Annotations;
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
    public List<Cell> Border = new List<Cell>();
    public GameObject _canvas;
    public List<Vector3> NeighbourType = new Neightbour().NeighbourType;
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

    private void Awake()
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
        foreach (GameObject gameObject in ObjectsFound)
        {
            if (gameObject.transform.name == "Floors")
            {
                foreach (Transform _cell2 in gameObject.GetComponentInParent<Transform>())
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
                        if(_cell2.GetComponent<Cell>().CanMove == true)
                        {
                            Cells.Add(_cell2.GetComponent<Cell>());
                        }                       
                    }
                }
            }
            if (gameObject.transform.name == "borders")
            {
                foreach(Transform _border in gameObject.GetComponentInParent<Transform>())
                {
                    if(_border.GetComponent<Cell>())
                    { 
                        Border.Add(_border.GetComponent<Cell>());
                    }
                }
            }



        }       
        TeamQueue();
    }

    public void OnPointerClickEvent(Cell cell, Unit UNIT)
    {
        List<Cell> CellSelect = cell.CellSelect;
        List<Cell> MovedAfterAttack = cell.MovedAfterAttack;
        bool canMove = true;
        foreach (Cell _cell in Cells)
        {
            if(_cell != cell)
            {
                _cell._choice = "Lock";
            }
        }
        cell._material = Resources.Load("Material/focus_material", typeof(Material)) as Material;
        cell.SetSelect();
        if (UNIT.Lady != true)
        {
       
                for (int i = 0; i < CellSelect.Count; i++)
                {
                     if(CellSelect[i].Unit != null)
                     {
                       if (CellSelect[i].Unit.Team != UNIT.Team)
                       {
                           var posit = cell.transform.position - CellSelect[i].transform.position;
                           var pos = cell.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                           bool CanKill = true;
             
                           foreach(Cell _cell in MovedAfterAttack)
                           {
                               if (_cell.Unit != null && _cell.transform.position == pos) { CanKill = false; }
                               if (_cell.transform.position == pos && _cell._choice == "border") { CanKill = false; }
                           }
 
                           if (CanKill == true)
                           {
                               canMove = false;
                               CellSelect[i]._choice = "isattack";
                               CellSelect[i].Unitselected = cell.Unit;
                               CellSelect[i]._material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                               CellSelect[i].SetSelect();
                           }
                          
                       }
                     }     

                                 

                }
                if(canMove == true)
                {
                 for (int i = 0; i < CellSelect.Count; i++)
                 {
                    if ((CellSelect[i].transform.position == cell.transform.position + NeighbourType[2] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[7]) && CellSelect[i].Unit == null && UNIT.Team == "Red")
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
                 }
                }







        }
        if(UNIT.Lady == true)
        {
            CellSelect = cell.CellSelectLady;
            MovedAfterAttack = cell.MovedAfterAttackLady;
            for(int i = 0; i < CellSelect.Count; i++)
            {
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
                        foreach (Cell _cell in MovedAfterAttack)
                        {
                            if (_cell.Unit != null && _cell.transform.position == pos) { CanKill = false; }
                            if (_cell.transform.position == pos && _cell._choice == "border") { CanKill = false; }
                        }

                        if (CanKill == true)
                        {

                            canMove = false;
                            CellSelect[i]._choice = "isattack";
                            CellSelect[i].Unitselected = cell.Unit;
                            CellSelect[i]._material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                            CellSelect[i].SetSelect();
                        }


                    }
                }
            }
            if(canMove == true)
            {
                for (int i = 0; i < CellSelect.Count; i++)
                {
                    if (CellSelect[i].Unit == null && CellSelect[i].CanMove == true)
                    {

                        CellSelect[i]._choice = "ismove";
                        CellSelect[i].Unitselected = cell.Unit;
                        CellSelect[i]._material = Resources.Load("Material/select_green", typeof(Material)) as Material;
                        CellSelect[i].SetSelect();

                    }

                    if (CellSelect[i].Unit != null)
                    {

                        var posit = cell.transform.position - CellSelect[i].transform.position;
                        foreach (Cell _cell in MovedAfterAttack)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                if (posit.x >= 1 && posit.z >= 1)
                                {
                                    if (_cell.transform.position == MovedAfterAttack[i].transform.position + new Vector3(-c, 0, -c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                                if (posit.x <= -1 && posit.z <= -1)
                                {
                                    if (_cell.transform.position == MovedAfterAttack[i].transform.position + new Vector3(c, 0, c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                                if (posit.x >= 1 && posit.z <= -1)
                                {
                                    if (_cell.transform.position == MovedAfterAttack[i].transform.position + new Vector3(-c, 0, c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                                if (posit.x <= -1 && posit.z >= 1)
                                {
                                    if (_cell.transform.position == MovedAfterAttack[i].transform.position + new Vector3(c, 0, -c))
                                    {
                                        Debug.Log(Convert.ToString(_cell.name));
                                        _cell._choice = "Lock";
                                        _cell.ResetSelect();
                                        _cell.Unitselected = null;
                                    }
                                }
                            }


                        }
                    }

                    

                }
            }
        }
        
    }



    public  void attacked(Cell cell, Unit UNIT)
    {
        foreach(Cell _cell in Cells)
        {
            _cell._choice = "Lock";
            _cell.ResetSelect();
        }
        List<Cell> CellSelect = cell.CellSelect;
        bool canMove = true;
        _canvas.GetComponent<BattleController>().canCancel = false;
        for (int i = 0; i < CellSelect.Count; i++)
        {
            if (CellSelect[i].Unit != null)
            {
                if (CellSelect[i].Unit.Team != UNIT.Team)
                {
                    var posit = cell.transform.position - CellSelect[i].transform.position;
                    var pos = cell.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                    bool CanKill = true;
                    foreach (Cell _cell in cell.MovedAfterAttack)
                    {
                        if (_cell.Unit != null && _cell.transform.position == pos) { CanKill = false; }
                        if (_cell.transform.position == pos && _cell._choice == "border") { CanKill = false; }
                    }

                    if (CanKill == true)
                    {
                        canMove = false;
                        CellSelect[i]._choice = "isattack";
                        CellSelect[i].Unitselected = cell.Unit;
                        CellSelect[i]._material = Resources.Load("Material/select_red", typeof(Material)) as Material;
                        CellSelect[i].SetSelect();
                    }

                }
            }

        }
        if (canMove == true)
        {
            _canvas.GetComponent<BattleController>().canCancel = true;
            if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Red")
            {
                _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Blue";
            }
            else if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Blue")
            {
                _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Red";
            }
            TeamQueue();
        }
    }

    public void attackedLady(Cell cell, Unit UNIT)
    {
       foreach (Cell _cell in Cells)
{
    _cell._choice = "Lock";
    _cell.ResetSelect();
}
List<Cell> CellSelect = cell.CellSelectLady;
List<Cell> MovedAfterAttack = cell.MovedAfterAttackLady;
bool canMove = true;
_canvas.GetComponent<BattleController>().canCancel = false;
for (int i = 0; i < CellSelect.Count; i++)
{
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
            foreach (Cell _cell in MovedAfterAttack)
            {
                if (_cell.Unit != null && _cell.transform.position == pos) { CanKill = false; }
                if (_cell.transform.position == pos && _cell._choice == "border") { CanKill = false; }
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

    if (CellSelect[i].Unit != null)
    {

        var posit = cell.transform.position - CellSelect[i].transform.position;
        foreach (Cell _cell in CellSelect)
        {
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

    }
}

        foreach (Cell _cell in CellSelect)
        {
            if(_cell._choice == "isattack")
            {
                canMove = false;
            }
        }

if (canMove == true)
{
    _canvas.GetComponent<BattleController>().canCancel = true;
    if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Red")
    {
        _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Blue";
    }
    else if (_canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text == "Blue")
    {
        _canvas.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetComponent<UnityEngine.UI.Text>().text = "Red";
    }
    TeamQueue();
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
        TeamQueue();
    }
    public void IsAttack(Cell cell, Unit UNIT)
    {  
                var posit = UNIT.transform.position - (cell.transform.position + new Vector3(0, 0.678f, 0));
                var pos = UNIT.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                List<Cell> CellSelect = cell.CellSelect;
                    if(UNIT.Lady == true)
                    {
                     CellSelect = cell.CellSelectLady;
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
                foreach (Cell _cell in CellSelect)
                {
                    if (_cell.transform.position == pos - new Vector3(0, 0.678f, 0))
                    {                       
                        UNIT.Cell = _cell;
                        _cell.Unit = UNIT;
                        if(UNIT.Lady == false)
                        {
                            attacked(_cell, UNIT);
                        }
                        else if (UNIT.Lady == true)
                        {
                            attackedLady(_cell, UNIT);
                        }
                        break;
                    }
                }

    }
    

}
