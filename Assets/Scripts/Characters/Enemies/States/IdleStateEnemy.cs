
public class IdleStateEnemy : State<StateEnemy>
{
    Enemy enemy;

    public IdleStateEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        state = StateEnemy.Idle;
    }

    public override void Enter()
    {
        enemy.animations.Move(0);
    }
  
}
