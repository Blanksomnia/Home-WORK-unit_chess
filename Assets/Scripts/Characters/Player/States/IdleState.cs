using UnityEngine;

public class IdleState : State<StatePlayer>
{
    Player player;

    public IdleState(Player player)
    {
        this.player = player;
        state = StatePlayer.Idle;
    }

    public override void Enter()
    {

        if (player.mode.onTheEdgeOfTheWall)
            player.animations.IsClimbing(0.5f);
        else
            player.animations.Move(0);
    }

    public override void Update(float deltaTime)
    {
        if(player.rb.velocity.magnitude > 0)
        player.rb.velocity = new Vector3(0, player.rb.velocity.y, 0);
    }


}
