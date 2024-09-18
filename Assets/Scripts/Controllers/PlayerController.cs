using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    BattleField BattleField;
    public void choice(Cell _cell, string _choice , Unit unit, Unit selun)
    {
        
        switch (_choice)
        {
            case "Lock":
                {
                    
                }
                break;
            case "select":
                {
                   BattleField.OnPointerClickEvent(_cell, unit);
                }
                break;
            case "ismove":
                {
                    BattleField.ActQueue(_cell, selun, "ismove");
                }
                break;
            case "isattack":
                {
                   BattleField.ActQueue(_cell, selun, "isattack");
                }
                break;
            default:
                break;
        }
    }
}
