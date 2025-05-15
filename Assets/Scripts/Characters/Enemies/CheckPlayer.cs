using UnityEngine;
using Random = System.Random;

public class CheckTarget
{
    Random rand = new Random();
    Enemy enemy;
    bool canMove = true;
    TimerDelegate timer;

    public CheckTarget(Enemy enemy)
    {
        this.enemy = enemy;
        timer = CanMoveToPoint;
    }

    public void Check()
    {
        if(enemy.target == null)
        {
            if (Vector2.Distance(new Vector2(enemy.transform.position.x, enemy.transform.position.z), new Vector2(enemy.moveTo.x, enemy.moveTo.z)) <= 1.3f || OtherUnitOnPoint() )
                enemy.moveTo = Vector3.zero;

            if(enemy.moveTo == Vector3.zero && canMove)
            Move();

            Find();
        }
        else
        {
            if (enemy.target.tag == "Player")
            {
                float distance = Vector3.Distance(enemy.transform.position, enemy.target.position);

                if (distance <= enemy.distanceLookAtPlayer)
                {
                    if (distance <= enemy.distanceToAttack)
                        enemy.moveTo = Vector3.zero;
                    else
                        enemy.moveTo = enemy.target.position;
                }
                else
                {
                    enemy.target = null;
                    enemy.moveTo = Vector3.zero;
                }

            }
            else
                enemy.target = null;

        }
    }

    private bool OtherUnitOnPoint()
    {
        Collider[] units = Physics.OverlapBox(enemy.transform.position + new Vector3(0, 3, 0), new Vector3(1.5f, 1.5f, 1.5f), Quaternion.identity, enemy.unit);
        if (units.Length > 0)
        {
            bool right = false;

            for(int i = 0; i < units.Length; i++)
            {
                if (units[i].transform != enemy.transform)
                    if (Vector3.Distance(units[i].transform.position, enemy.moveTo) <= 1.5f)
                        right = true;
            }

            return right;
        }
        else
         return false; 
    }


    private void Find()
    {
        Collider[] targets = Physics.OverlapSphere(enemy.transform.position, enemy.distanceLookAtPlayer, enemy.unit);

        if(targets.Length > 0 )
            for(int i = 0; i < targets.Length; i++)
                if(targets[i].tag == "Player")
                {             
                    Vector3 direction = targets[i].transform.position - enemy.transform.position;

                    if (!Physics.Raycast(enemy.transform.position + new Vector3(0, 3F, 0), direction, Vector3.Distance(enemy.transform.position, targets[i].transform.position), enemy.obstical))
                    {
                        enemy.target = targets[i].transform;
                        break;
                    }
                }
               
    }

    private void Move()
    {
        int index = rand.Next(0, enemy.pointsToMove.Count);
        enemy.moveTo = enemy.pointsToMove[index].position;
        canMove = false;
        enemy.timer.Add(new Timer(enemy.durationMoveToPoints, null, timer));
    }

    private void CanMoveToPoint()
    {
        canMove = true;
    }
}
