using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonCell : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI value;
    [SerializeField] TextMeshProUGUI nameCell;

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


    public void GetValue(int val) { value.text = val.ToString(); }
    public void GetName(TypeUnits name) { nameCell.text = ConvertToName(name); }

}
