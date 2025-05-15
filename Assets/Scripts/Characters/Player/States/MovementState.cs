using UnityEngine;

public class MovementState : State<StatePlayer>
{
    private Player player;

    public MovementState(Player player)
    {
        this.player = player;
        state = StatePlayer.Movement;
    }

    public override void Enter()
    {
        if (!player.mode.inAir)
            player.sound.StartMove();

        player.animations.Move(1);
    }

    public override void Update(float deltaTime)
    {
         IsMoving();
    }

    private void IsMoving()
    {
        Vector3 current = new Vector3(player.turnMove.x, 0, player.turnMove.y);
        Vector3 target = player.rb.transform.position;
        target.x += current.x;
        target.z += current.z;

        player.animations.LookAt(target, 460);
        float speed = player.speed;
        player.rb.AddForce(current * speed * 1000 * player.rb.mass);
            
        if (player.rb.velocity.magnitude > speed)
        {
            Vector3 total = player.rb.velocity.normalized * speed;
            total.y = player.rb.velocity.y;
            player.rb.velocity = total;
        }

    }

    public override void Exit()
    {
        player.sound.Stop();
    }
}
