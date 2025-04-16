using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : Movement
{
    private Player player;

    public DeadState(Player player, StatePlayer st)
    {
        this.player = player;
        state = st;
    }
    public override void Enter()
    {
        player.enabled = false;
    }


}
