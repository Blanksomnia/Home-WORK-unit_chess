using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfoEdit : ITableInfo
{

    public void CloseInfo(GameObject Info)
    {
        Info.SetActive(false);
    }
    public void OpenInfo(GameObject Info)
    {
        Info.SetActive(true);
    }

    public void GiveInfoMain(string name, TextMeshProUGUI main)
    {
        main.text = "Name: " + name;
    }
    public void GiveInfoDescription(int damage, int health, int ammo,TextMeshProUGUI description)
    {
        string Description = null;

        if (health > 0)
        {
            Description = "Health: " + health;
        }
        

        if (damage > 0)
        {
            Description += "Damage: " + damage + " Ammo: " + ammo;
            
        }

        if (health <= 0 && damage <= 0)
        {
            Description = "Status - Dead";
        }

        description.text = Description;
    }

}

