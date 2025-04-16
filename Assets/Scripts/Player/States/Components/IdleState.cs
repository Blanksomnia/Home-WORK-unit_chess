using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : Movement
{
    Player player;

    public IdleState(Player player, StatePlayer st)
    {
        this.player = player;
        state = st;
    }

    public override void Enter()
    {
        if (!player.inAir)
            player.animations.Move(0);
        if (player.canClimb)
        {
            player.animations.IsClimbing(0);
        }
    }





}
