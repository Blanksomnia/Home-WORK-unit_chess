using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    public Sprite unknown;
    public Sprite blaster;
    public Sprite healthPoison;
    public Sprite speedPoison;
    public Sprite emptyPoison;
    public Sprite keyRed;
    public Sprite keyYellow;
    public Sprite keyBlue;
    public Sprite puzzle;

    public TextMeshProUGUI textSelected;
    public Image centerCell;


    public void SelectItem(string selected, int select)
    {
        select++;
        centerCell.sprite = GetSprite(selected);
        textSelected.text = select.ToString();
    }



    private Sprite GetSprite(string name)
    {
        switch (name)
        {
            case "Blaster": return blaster;
            case "Health poison": return healthPoison;
            case "Speed poison": return speedPoison;
            case "Red key": return keyRed;
            case "Blue key": return keyBlue;
            case "Yellow key": return keyYellow;
            case "Empty poison": return emptyPoison;
            case "Empty blaster": return blaster;
            case "Puzzle": return puzzle;
            default: return unknown;
        }
    }

}
