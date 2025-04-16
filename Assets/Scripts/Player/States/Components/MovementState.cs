using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : Movement
{
    private Player player;

    public MovementState(Player player, StatePlayer st)
    {
        this.player = player;
        state = st;
    }

    public override void Enter()
    {
        if (!player.inAir)
        player.animations.Move(1);
    }

    public override void Update()
    {

        if (!player.inAir)
         IsMoving(player.speedDefault, player.turnMove);
        else{ IsMoving(player.speedInAir, player.turnMove); }

    }

    private void IsMoving(float speed, Vector2 move)
    {
        Vector3 current = new Vector3(move.x, 0, move.y);
        Vector3 target = player.rb.transform.position;
        target.x += move.x;
        target.z += move.y;

        player.animations.LookAt(target, 360);
        player.rb.AddForce(current * player.rb.mass * 1000 * speed);


            if (player.rb.velocity.magnitude > speed && !player.inAir)
            {
                Vector3 total = player.rb.velocity.normalized * speed;
                total.y = player.rb.velocity.y;
                player.rb.velocity = total;
            }

    }

}
