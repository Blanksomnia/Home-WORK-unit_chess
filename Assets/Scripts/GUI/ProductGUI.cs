using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProductGUI : MonoBehaviour
{
    TextMeshProUGUI Cost;
    [SerializeField] Product product;

    private void Awake()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ConvertToName(product._cell._type);
        Cost = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        Cost.text = ConvertCostToString(product._cost);
    }

    private string ConvertCostToString(List<MaterialMine> mines)
    {
        string totat = "Cost : ";
        for (int i = 0; i < mines.Count; i++)
        {
            totat += ConvertMine(mines[i]);
        }

        return totat;
    }

    private string ConvertMine(MaterialMine mine)
    {
        switch (mine._type)
        {
            case TypeMine.Stone: { } return "Stone - " + mine._value + "; ";
            case TypeMine.Coal: { } return "Coal - " + mine._value + "; ";
            case TypeMine.Gold: { } return "Gold - " + mine._value + "; ";
                default: return "Empty - " + mine._value;
        }

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

}
