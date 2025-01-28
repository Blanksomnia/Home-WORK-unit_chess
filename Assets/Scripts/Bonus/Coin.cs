using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class Coin : Bonus
{

    public override void Detect()
    {
        player.GetCoin(1);

    }



}
