using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : Movement
{
    private Player player;

    public JumpState(Player player, StatePlayer st)
    {
        this.player = player;
        state = st;
    }

    public override void Enter()
    {
    
        if(player.canClimb)
        {
            player.rb.isKinematic = false;
            player.LockMove = true;
        }
        player.animations.StartJump();
        IsJumping(player.powerJump);

    }



    private void IsJumping(float strenghJump)
    {
        Vector3 current = Vector3.zero;
        current.x = player.turnToJumpAfterClimb.x;
        current.y += strenghJump;
        current.z = player.turnToJumpAfterClimb.y;

        if(player.turnToJumpAfterClimb != Vector2.zero)
        player.animations.LookAt(player.rb.transform.position + new Vector3(player.turnToJumpAfterClimb.x, 0, player.turnToJumpAfterClimb.y), 360);

        player.turnToJumpAfterClimb = Vector2.zero;

        player.rb.AddForce(current * player.rb.mass * 1000);
    }

}
