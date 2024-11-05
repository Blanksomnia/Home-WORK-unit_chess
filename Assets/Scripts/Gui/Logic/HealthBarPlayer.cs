using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : IHealtbar
{
    public void HealthBarEdit(Image bar, int health, int maxhealth)
    {
        bar.fillAmount = (float)health / maxhealth;
    }
}
