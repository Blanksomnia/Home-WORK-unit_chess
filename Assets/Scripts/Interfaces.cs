using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum status
{
    Castle,
    NearCastle,
    Outside,
    Down,
    None
}
interface IWayStatus
{
    void ChangeToNotFoundWay();
    void ChangeToFoundWay(string variant);
}
