using UnityEngine;

public class JumpState : State<StatePlayer>
{
    private Player player;

    public JumpState(Player player)
    {
        this.player = player;
        state = StatePlayer.Jump;
    }

    public override void Enter()
    {
        if (player.mode.onTheEdgeOfTheWall)
        {
            player.mode.onTheEdgeOfTheWall = false;
            player.mode.lockChangeClimb = true;
            player.animations.StartJump();
        }

        IsJumping();
    }

    private void IsJumping()
    {
        Vector3 current = Vector3.zero;
        current.x = player.turnMove.x;
        current.y += player.powerJump;
        current.z += player.turnMove.y;

        if(player.turnToJump != Vector2.zero)
        {
            player.animations.LookAt(player.rb.transform.position + new Vector3(player.turnToJump.x, 0, player.turnToJump.y), 600);
            current.x = player.turnToJump.x;
            current.z = player.turnToJump.y;
        }

        player.rb.AddForce(current * player.rb.mass * 1000);
    }

    public override void Exit()
    {
        player.turnToJump = Vector2.zero;
    }

}
