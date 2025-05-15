using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthbarView : MonoBehaviour
{
    public TextMeshProUGUI valueHealth;

    public void ChangeValue(int value)
    {
        valueHealth.text = ": " + value;
    }
}
