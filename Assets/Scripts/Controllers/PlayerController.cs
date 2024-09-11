using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
                    gameObject.GetComponent<BattleField>().OnPointerClickEvent(_cell, unit);
                }
                break;
            case "ismove":
                {
                    gameObject.GetComponent<BattleField>().f(_cell, selun, "ismove");
                }
                break;
            case "isattack":
                {
                    gameObject.GetComponent<BattleField>().f(_cell, selun, "isattack");
                }
                break;
            default:
                break;
        }
    }
}
