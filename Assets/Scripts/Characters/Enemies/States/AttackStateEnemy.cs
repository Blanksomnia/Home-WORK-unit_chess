using UnityEngine;

public class AttackStateEnemy : State<StateEnemy>
{
    Enemy enemy;
    Quaternion rotateSpineStarted = Quaternion.identity;

    public AttackStateEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        state = StateEnemy.Attack;
    }


    public override void Enter()
    {
        if(rotateSpineStarted == Quaternion.identity)
            rotateSpineStarted = enemy.spine.localRotation;

        enemy.animations.Move(0);
    }

    public override void Update(float deltaTime)
    {
        enemy.spine.LookAt(enemy.target);
        enemy.transform.LookAt(new Vector3(enemy.target.position.x, enemy.transform.position.y, enemy.target.position.z));
        enemy.inventory.ActivateItem(enemy);
    }

    public override void Exit()
    {
        enemy.spine.localRotation = rotateSpineStarted;
    }

}
