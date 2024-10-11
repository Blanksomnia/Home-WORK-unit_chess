using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int _score = 0;
    TextMeshProUGUI _textMeshPro;
    public int attempts;
    public List<skittle> skittles= new List<skittle>();

    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void GiveScore()
    {
        if (attempts == 1 && _score == 10)
        {
            attempts = 0;
            _score += 5;
            foreach (skittle skittle in skittles)
            {
                skittle.Reset = true;
            }
        }
        if (attempts == 2 && _score == 10)
        {
            _score += 2;
            attempts = 0;
            foreach (skittle skittle in skittles)
            {
                skittle.Reset = true;
            }
        }
        if (attempts == 3 && _score == 10)
        {
            attempts = 0;
            foreach (skittle skittle in skittles)
            {
                skittle.Reset = true;
            }
        }
         _textMeshPro.text = Convert.ToString(_score);
    }
    // Update is called once per frame
}
