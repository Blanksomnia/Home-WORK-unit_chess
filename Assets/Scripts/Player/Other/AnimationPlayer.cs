using System;
using UnityEngine;
using Random = System.Random;

public class AnimationPlayer
{
    Random rand = new Random();
    Player player;
    Animator animator;

    private int jump = 1;
    private int idle = 0;
    private int move = 2;
    private int startClimb = 3;
    private int climbMove = 4;
    private int endClimb = 5;

    private int frameJump = 15;

    private float speedRot;
    bool Rotation = false;
    Vector3 rotTurn = Vector3.zero;
    Quaternion totalRot = Quaternion.identity;

    Vector3 posPlayerClimb = Vector3.zero;

    public AnimationPlayer(Player player)
    {
        this.player = player;
        animator = player.GetComponent<Animator>();
    }

    public void Idle() { animator.Play(idle); }
    public void Move(float value) { animator.SetFloat(move, value); }
    public void StartJump() { animator.Play(jump); }
    public void EndJump() { animator.StartRecording(frameJump); player.LockMove = false; }

    public void StartClimb()
    {
        animator.Play(startClimb);
        player.rb.isKinematic = true;
    }

    public void IsClimbing(float value)
    {
        animator.SetFloat(climbMove, value);
    }

    public void EndClimb(Vector3 posPlayerFinish)
    {
        animator.Play(endClimb);
        this.posPlayerClimb = posPlayerFinish;
        //player.LockMove = true;

        animator.transform.position = posPlayerClimb;
        player.LockJump = false;
        player.LockMove = false;
        player.rb.isKinematic = false;
        player.turnToJumpAfterClimb = Vector2.zero;
    }


    public void Update()
    {
        if(Rotation)
        {
            IsRotation();
        }
 
    }

    private void IsRotation()
    {
        float dist = Vector3.Distance(totalRot * Vector3.forward, animator.transform.forward);
        if (dist < 0.2f)
        {

            animator.transform.rotation = totalRot;
            Rotation = false;
        }
        else
        {
            animator.transform.Rotate(rotTurn * speedRot * Time.deltaTime);
        }
    }

    public void LookAt(Vector3 to, float speed)
    {
        float total = Quaternion.FromToRotation(Vector3.right, to - animator.transform.position).eulerAngles.y;
        total += 90;
        speedRot = speed;

        Rotate(total);

    }

    private void Rotate(float toY)
    {

        Quaternion TURN = Quaternion.Euler(new Vector3(0, toY, 0));
        Vector3 forwardTotal = TURN * Vector3.forward;

        if (animator.transform.rotation != TURN)
        {
            totalRot = TURN;

            Vector3 leftTurn = Vector3.down;
            Vector3 rightTurn = Vector3.up;

            Vector3 near = TURN * Vector3.back;

            int random = rand.Next(0, 2);

            if (random == 0)
            {
                rotTurn = leftTurn;
            }
            else
            {
                rotTurn = rightTurn;
            }

            if (Vector3.Distance(animator.transform.right, forwardTotal) < Vector3.Distance(animator.transform.right, near))
            {
                rotTurn = rightTurn;
            }

            if (Vector3.Distance(-animator.transform.right, forwardTotal) < Vector3.Distance(-animator.transform.right, near))
            {
                rotTurn = leftTurn;
            }

            Rotation = true;
        }

    }

    private void EventStopJump()
    {
        animator.StopRecording();

    }

    private void EventEndAnimIsClimbing()
    {
        animator.transform.position = posPlayerClimb;
        player.LockJump = false;
        player.rb.isKinematic = false;
    }
}
