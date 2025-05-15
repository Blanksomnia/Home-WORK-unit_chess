using UnityEngine;

public class ClimbState : State<StatePlayer>
{
    private Player player;
    Vector3 posPlayerAfterClimb = Vector3.zero;
    float secondsToEndAnimClimb = 2f;
    TimerDelegate timer;

    public ClimbState(Player player)
    {
        this.player = player;
        state = StatePlayer.Climb;
        timer += EventEndAnimIsClimbing;
    }

    public override void Enter()
    {
        player.animations.IsClimbing(0.5f);
    }
    public override void Update(float deltaTime)
    {
        Movement(deltaTime);
    }


    private void Movement(float deltaTime)
    {
        Vector2 move = player.turnMove;
        if (move != Vector2.zero)
        {
            Vector3 turnToJumpAfterClimb = Vector2.zero;
            Vector3 left = -player.rb.transform.right;
            Vector3 right = player.rb.transform.right;
            Vector3 moveTo = Vector3.zero;
            Vector3 target = player.rb.transform.position;
            target.x += move.x;
            target.z += move.y;


            if (LookAt(Vector3.forward + player.rb.transform.position) == LookAt(target) && player.checks.ClimbUpCheck())
            {
                posPlayerAfterClimb = player.transform.position + (player.transform.forward * 1f)  + new Vector3(0, 4f, 0);
                player.lockJump = true;
                player.lockMove = true;
                player.mode.lockChangeClimb = true;
                player.transform.position += new Vector3(0, 0.2f, 0);
                player.timerManager.Add(new Timer(secondsToEndAnimClimb, null, timer));
                player.animations.EndClimb();

            }
            else if (LookAt(target) == LookAt(player.rb.transform.position + Vector3.right))
            {
                player.lockJump = false;
                turnToJumpAfterClimb = right * 0.15f;

                if (player.checks.ClimbRightCheck())
                {
                    moveTo = right * player.speedClimb * deltaTime;
                    player.animations.IsClimbing(1);
                }
                else
                    player.animations.IsClimbing(0.5f);



            }
            else if (LookAt(target) == LookAt(player.rb.transform.position + Vector3.left))
            {
                player.lockJump = false;
                turnToJumpAfterClimb = left * 0.15f;

                if (player.checks.ClimbLeftCheck())
                {
                    moveTo = left * player.speedClimb * deltaTime;
                    player.animations.IsClimbing(0);
                }
                else
                    player.animations.IsClimbing(0.5f);

            }
            else if (LookAt(player.rb.transform.position + Vector3.back) == LookAt(target))
            {
                player.lockJump = false;
                turnToJumpAfterClimb = -player.rb.transform.forward * player.speedClimb;
            }

            player.turnToJump = new Vector2(turnToJumpAfterClimb.x, turnToJumpAfterClimb.z);
            player.rb.transform.position += moveTo;

        }

    }

    private float LookAt(Vector3 to)
    {
        float total = Quaternion.FromToRotation(Vector3.right, to - player.rb.transform.position).eulerAngles.y;
        total += 90;
        return total;
    }

    public void EventEndAnimIsClimbing()
    {
        player.transform.position = posPlayerAfterClimb;
        player.lockJump = false;
        player.lockMove = false;
        player.mode.lockChangeClimb = false;
        player.mode.onTheEdgeOfTheWall = false;
    }
}
