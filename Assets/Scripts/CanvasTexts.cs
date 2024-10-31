using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CanvasTexts : MonoBehaviour, IWayStatus
{
    [SerializeField] private TextMeshProUGUI IWayText;

    public void ChangeToNotFoundWay()
    {
        IWayText.text = "Choose a way";
    }
    public void ChangeToFoundWay(string variant)
    {
        IWayText.text = variant;
    }
}
