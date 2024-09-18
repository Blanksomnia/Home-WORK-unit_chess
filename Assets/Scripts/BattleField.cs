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
    List<Vector3> NeighbourType = new Neightbour().NeighbourType;

    [SerializeField]
    BattleController BattleController;

    [SerializeField]
    UnityEngine.UI.Text UITeamText;

    [SerializeField]
    Material SelectGreen;
    [SerializeField]
    Material FocusGreen;
    [SerializeField]
    Material SelectRed;

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
            if (_unit.Team == UITeamText.text)
            {
                _unit.Cell._choice = "select";
            }
        }
        

    }

    private void Awake()
    {
        GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();

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

        cell._material = FocusGreen;
        cell.SetSelect();

        if (UNIT.Lady != true)
        {

            for (int i = 0; i < CellSelect.Count; i++)
            {
                if (CellSelect[i].Unit != null)
                {
                    if (CellSelect[i].Unit.Team != UNIT.Team)
                    {
                        var posit = cell.transform.position - CellSelect[i].transform.position;
                        var pos = cell.transform.position + new Vector3(-posit.x * 2, 0, -posit.z * 2);
                        bool CanKill = true;

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
                            CellSelect[i]._material = SelectRed;
                            CellSelect[i].SetSelect();
                        }

                    }
                }



            }

            if (canMove == true)
            {
                for (int i = 0; i < CellSelect.Count; i++)
                {
                    if ((CellSelect[i].transform.position == cell.transform.position + NeighbourType[2] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[7]) && CellSelect[i].Unit == null && UNIT.Team == "Red")
                    {

                        CellSelect[i]._choice = "ismove";
                        CellSelect[i].Unitselected = cell.Unit;
                        CellSelect[i]._material = SelectGreen;
                        CellSelect[i].SetSelect();

                    }
                    else if ((CellSelect[i].transform.position == cell.transform.position + NeighbourType[1] || CellSelect[i].transform.position == cell.transform.position + NeighbourType[3]) && CellSelect[i].Unit == null && UNIT.Team == "Blue")
                    {
                        CellSelect[i]._choice = "ismove";
                        CellSelect[i].Unitselected = cell.Unit;
                        CellSelect[i]._material = SelectGreen;
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
                            CellSelect[i]._material = SelectRed;
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
                        CellSelect[i]._material = SelectGreen;
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
        BattleController.canCancel = false;

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
                        CellSelect[i]._material = SelectRed;
                        CellSelect[i].SetSelect();
                    }

                }
            }

        }

        if (canMove == true)
        {
            BattleController.canCancel = true;
            if (UITeamText.text == "Red")
            {
                UITeamText.text = "Blue";
            }
            else if (UITeamText.text == "Blue")
            {
                UITeamText.text = "Red";
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
        BattleController.canCancel = false;

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
                        CellSelect[i]._material = SelectRed;
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
            if (_cell._choice == "isattack")
            {
                canMove = false;
            }
        }

        if (canMove == true)
        {
            BattleController.canCancel = true;
            if (UITeamText.text == "Red")
            {
                UITeamText.text = "Blue";
            }
            else if (UITeamText.text == "Blue")
            {
                UITeamText.text = "Red";
            }
            TeamQueue();
        }
    }

    public void ActQueue(Cell cell, Unit UNIT, string f)
    {
        if (f == "ismove")
        {
           BattleController.CELL = cell;
           BattleController.UNIT = UNIT;
           BattleController.conf = true;
        }
        if (f == "isattack")
        {
            BattleController.CELL = cell;
            BattleController.UNIT = UNIT;
            BattleController.conf = false;
        }
    }

    public void IsMoved(Cell cell, Unit UNIT)
    {
        cell.Unitselected = null;

        if (cell.Unit == null)
        {
            if (UITeamText.text == "Red")
            {
                UITeamText.text = "Blue";
            }
            else if (UITeamText.text == "Blue")
            {
                UITeamText.text = "Red";
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

        if (UNIT.Lady == true)
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
        cell.Unit.Team = "dead";
        cell.Unit = null;

        foreach (Cell _cell in CellSelect)
        {
            if (_cell.transform.position == pos - new Vector3(0, 0.678f, 0))
            {
                UNIT.Cell = _cell;
                _cell.Unit = UNIT;
                if (UNIT.Lady == false)
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
