using UnityEngine;

public class PlayerController
{
    public void choice(GameObject gameObject, string _choice)
    {
        switch (_choice)
        {
            case "Lock":
                {
                    
                }
                break;
            case "select":
                {
                    new BattleField().OnPointerClickEvent(gameObject);
                }
                break;
            case "ismove":
                {
                    new BattleField().IsMoved(gameObject);
                }
                break;
            case "isattack":
                {
                    new BattleField().IsAttack(gameObject);
                }
                break;
            default:
                break;
        }
    }
}
