using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    [SerializeField]
    Material Blue;
    [SerializeField]
    Material Red;

    public string Team;

    public bool Lady = false;

    public Cell Cell;

    MeshRenderer UnitRender;

    // Start is called before the first frame update
    void Awake()
    {
        UnitRender = gameObject.GetComponent<MeshRenderer>();
    }
    public void BecomeLady()
    {
        if (Team == "Red" && Cell.becomeLadyRed == true && Cell != null)
        {
            Lady = true;
            UnitRender.material = Red;
        }
        if (Team == "Blue" && Cell.becomeLadyBlue == true && Cell != null)
        {
            Lady = true;
            UnitRender.material = Blue;
        }
    }
    public void Move(UnityEngine.Vector3 pos)
    {
        gameObject.transform.position = pos;
    }
    
}
