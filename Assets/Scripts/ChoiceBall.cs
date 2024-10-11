using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceBall : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] BallScript _ballScript;
    [SerializeField] TextMeshProUGUI _textMeshPro;
    int choice = 1;
    // Start is called before the first frame update
    bool Mouse;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Mouse == true)
        {
            if (_textMeshPro.text == "Ball blue")
            {
                _textMeshPro.text = "Ball yellow";
                choice = 0;
            }
            else if (_textMeshPro.text == "Ball yellow")
            {
                _textMeshPro.text = "Ball blue";
                choice = 1;
            }
            _ballScript.BallType(choice);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Mouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Mouse = false;
    }


}
