using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : Movement 
{
    private Player player;

    public AttackState(Player player, StatePlayer st)
    {
        this.player = player;
        state = st;
    }

    public override void Enter()
    {
        if(player.enemy != null)
        {
            player.enemy._health -= player.damage;
        }
    }

    public override void Exit()
    {
        player.enemy = null;
    }
}
