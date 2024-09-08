using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public string _choice = "select";
    public GameObject _unit = null;
    public GameObject Unit;
    public bool becomeLadyRed;
    public bool becomeLadyBlue;
    public string Team;
    [SerializeField]
    public bool CanMove;

    void Update()
    {

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.Find("focus").GetComponent<MeshRenderer>().enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        new PlayerController().choice(gameObject, _choice);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        gameObject.transform.Find("focus").GetComponent<MeshRenderer>().enabled = false;
    }
    public void SetSelect(Material _material)
    {
        gameObject.transform.Find("select").GetComponent<MeshRenderer>().material = _material;
        gameObject.transform.Find("select").GetComponent<MeshRenderer>().enabled = true;



    }
    public void ResetSelect()
    {
        gameObject.transform.Find("select").GetComponent<MeshRenderer>().enabled = false;
    }

}
