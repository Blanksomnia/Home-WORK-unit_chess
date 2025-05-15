
public class AttackState : State<StatePlayer>
{
    private Player player;
    float duration = 0.5f;
    TimerDelegate timer;

    public AttackState(Player player)
    {
        this.player = player;
        state = StatePlayer.AttackInAir;
        timer += CanAttack;
    }

    public override void Enter()
    {
        player.updates.characters.GetDamage(player.enemy, player.damage);
        player.timerManager.Add(new Timer(duration, null, timer));
        player.lockAttack = true;
    }

    public override void Exit()
    {
        player.enemy = null;
        player.turnToJump = player.transform.forward * 3f;
    }

    private void CanAttack()
    {
        player.lockAttack = false;
    }
}
