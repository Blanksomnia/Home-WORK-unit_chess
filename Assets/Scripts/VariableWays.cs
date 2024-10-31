using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableWays : MonoBehaviour
{
    [SerializeField] private Player _player;
    public string ÑhoiceInfoWay(status variant)
    {
        switch(variant)
        {
            case status.Castle:  return "The way to the castle";
            case status.NearCastle: return "The way Near castle";
            case status.Outside: return "The way to outside";
            case status.Down: return "The way down";

            default: return "error";
        }
    }

    public void ÑhoiceToMove(status variant)
    {
        switch (variant)
        {
            case status.Castle: _player.MoveToCastle(); break;
            case status.NearCastle: _player.MoveNearCastle(); break;
            case status.Outside: _player.MoveToOutside(); break;
            case status.Down: _player.MoveDown(); break;
            case status.None: break;

            default: break;
        }
    }

}
