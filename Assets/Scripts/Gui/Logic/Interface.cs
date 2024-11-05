using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface ITableInfo
{

    public void CloseInfo(GameObject Info);
    public void OpenInfo(GameObject Info);
    public void GiveInfoMain(string name, TextMeshProUGUI main);
    public void GiveInfoDescription(int damage, int health, int ammo, TextMeshProUGUI description);


}

public interface IHealtbar
{
    public void HealthBarEdit(Image bar, int health, int maxhealth);
}
