using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AnimationCharacter
{
    Animator animator;
    Random rand = new Random();

    private float speedRot;
    bool rotation = false;
    Vector3 rotTurn = Vector3.zero;
    Quaternion totalRot = Quaternion.identity;

    private int move = -281135240;
    private int jumpStart = 819769265;
    private int jumpEnd = -342501864;
    private int startClimb = -522766416;
    private int climbMove = 1571378954;
    private int endClimb = 374178362;
    private int dead = 1293411866;
    private int startUseItem = -1087796704;
    private int startUseBlaster = 426732286;
    private int getDamage = 2070990974;
    private int startUseArms = 1288071446;
    private int punch = -1319624832;

    public AnimationCharacter(Animator animator)
    {
        this.animator = animator;
    }


    public void Move(float value) => animator.SetFloat(move, value);
    public void StartJump() => animator.Play(jumpStart);
    public void EndJump() => animator.Play(jumpEnd);
    public void StartClimb() { animator.Play(startClimb); animator.Play(startClimb, 1); }
    public void IsClimbing(float value) => animator.SetFloat(climbMove, value);
    public void EndClimb() { animator.Play(endClimb); animator.Play(endClimb, 1); }
    public void Dead() { animator.Play(dead); animator.Play(dead, 1); }

    public void ChangeArm(string select, int num)
    {
        if (select == null)
            animator.Play(move, 1);
        else if (select == "Blaster")
            animator.Play(startUseBlaster, 1);
        else if(select == "Arms")
            animator.Play(startUseArms, 1);
        else
            animator.Play(startUseItem, 1);
    }

    public void ActivateItem(List<Effect> effects ,string select) 
    { 
        if (select == "Arms")
            animator.Play(punch, 1);
    }

    public void GetDamage() => animator.Play(getDamage, 2);

    public void Update(float deltaTime)
    {
        if (rotation)
            IsRotation(deltaTime);
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
        Quaternion turn = Quaternion.Euler(new Vector3(0, toY, 0));
        Vector3 forwardTotal = turn * Vector3.forward;

        if (animator.transform.rotation != turn)
        {
            totalRot = turn;

            Vector3 leftTurn = Vector3.down;
            Vector3 rightTurn = Vector3.up;
            Vector3 near = turn * Vector3.back;

            int random = rand.Next(0, 2);

            if (random == 0)
                rotTurn = leftTurn;
            else
                rotTurn = rightTurn;

            if (Vector3.Distance(animator.transform.right, forwardTotal) < Vector3.Distance(animator.transform.right, near))
                rotTurn = rightTurn;

            if (Vector3.Distance(-animator.transform.right, forwardTotal) < Vector3.Distance(-animator.transform.right, near))
                rotTurn = leftTurn;

            rotation = true;
        }

    }

    private void IsRotation(float deltaTime)
    {
        float dist = Vector3.Distance(totalRot * Vector3.forward, animator.transform.forward);
        if (dist < 0.2f)
        {
            animator.transform.rotation = totalRot;
            rotation = false;
        }
        else
            animator.transform.Rotate(rotTurn * speedRot * deltaTime);
    }
}
