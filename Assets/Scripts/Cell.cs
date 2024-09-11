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
    public Unit Unit;
    public Unit Unitselected;
    public bool becomeLadyRed;
    public bool becomeLadyBlue;
    [SerializeField]
    public bool CanMove;
    public GameObject _canvas;
    public Material _material;
    void Awake()
    {
        GameObject[] ObjectsFound = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject _object in ObjectsFound)
        {
            if (_object.transform.name == "Canvas")
            {
                _canvas = _object;

            }

        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.Find("focus").GetComponent<MeshRenderer>().enabled = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        _canvas.GetComponent<PlayerController>().choice(gameObject.GetComponent<Cell>(), _choice, Unit, Unitselected);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

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
