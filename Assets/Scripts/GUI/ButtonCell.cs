using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonCell : MonoBehaviour
{
    [SerializeField] CellUnit cell;
    [SerializeField] TextMeshProUGUI value;

    private void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ConvertToName(cell._type);
        GetValue();
    }

    private string ConvertToName(TypeUnits type)
    {
        switch (type)
        {
            case TypeUnits.Worker: { } return "Worker";
            case TypeUnits.Knight: { } return "Knight";
            case TypeUnits.Enemy: { } return "Enemy";
            default: return "Empty";
        }
    }


    public void GetValue() { value.text = cell._value.ToString(); }


}
