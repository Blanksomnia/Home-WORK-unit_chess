
using UnityEngine;

public class DeadStateEnemy : State<StateEnemy>
{
    Enemy enemy;
    public DeadStateEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        state = StateEnemy.Dead;
    }

    public override void Enter()
    {
        enemy.sound.Dead();
        enemy.animations.Dead();
        enemy.tag = "Dead";
        enemy.capsule.enabled = false;
        enemy.box.enabled = false;
        enemy.agent.Stop();
        enemy.agent.enabled = false;
    }
}
