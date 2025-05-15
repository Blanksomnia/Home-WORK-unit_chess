using UnityEngine;
public class MoveStateEnemy : State<StateEnemy>
{
    Enemy enemy;
    Vector3 to = Vector3.zero;
    public MoveStateEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        state = StateEnemy.Move;
    }

    public override void Enter()
    {
        enemy.agent.speed = enemy.speed;
        Move();
        enemy.animations.Move(1);
        enemy.sound.StartMove();
   
    }

    private void Move()
    {
        if(to != enemy.moveTo && enemy.moveTo != Vector3.zero) 
        {
            to = enemy.moveTo;
            enemy.agent.SetDestination(to);
        }
    }

    public override void Update(float deltaTime)
    {
        Move();
    }


    public override void Exit()
    {
        enemy.agent.speed = 0;
        enemy.sound.Stop();
    }
}
