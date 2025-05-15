using UnityEngine;
public class DeadState : State<StatePlayer>
{
    private Player player;

    public DeadState(Player player)
    {
        this.player = player;
        state = StatePlayer.Dead;
    }
    public override void Enter()
    {
        player.animations.Dead();
        player.sound.Dead();
        player.tag = "Dead";
        player.turnMove = Vector2.zero;
        player.enabled = false;
    }

}
