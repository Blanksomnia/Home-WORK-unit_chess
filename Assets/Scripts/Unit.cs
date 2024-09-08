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
    public bool IsSelected = false;
    public bool Lady = false;
    public GameObject Cell;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameObject.GetComponent<Renderer>().material == Blue)
        {
            Team = "Blue";

        }
        else if(gameObject.GetComponent<Renderer>().material == Red)
        {
            Team = "Red";
        }
    }
    void Update()
    {
        if (Team == "Red" && Cell.GetComponent<Cell>().becomeLadyRed == true)
        {
            Lady = true;
            gameObject.GetComponent<MeshRenderer>().material = Red;
        }
        if (Team == "Blue" && Cell.GetComponent<Cell>().becomeLadyBlue == true)
        {
            Lady = true;
            gameObject.GetComponent<MeshRenderer>().material = Blue;
        }
    }
    public void Move(UnityEngine.Vector3 ce)
    {
        gameObject.transform.position = ce;
    }
    
}
