using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectWay : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private CanvasTexts _canvasTexts;
    [SerializeField] private VariableWays _variablesWays;

    [SerializeField] private status _variantWay;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _canvasTexts.ChangeToFoundWay(_variablesWays.ÑhoiceInfoWay(_variantWay));
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        _variablesWays.ÑhoiceToMove(_variantWay);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _canvasTexts.ChangeToNotFoundWay();
    }
}
