using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbState : Movement
{
    private Player player;
    public ClimbState(Player player, StatePlayer st)
    {
        this.player = player;
        state = st;
    }

    private float LookAt(Vector3 to)
    {
        float total = Quaternion.FromToRotation(Vector3.right, to - player.rb.transform.position).eulerAngles.y;
        total += 90;
        return total;
    }


    public override void Enter()
    {
        player.LockJump = true;
    }
    public override void Update()
    {
        Movement();
    }


    private void Movement()
    {
        Vector2 move = player.turnMove;
        if (move != Vector2.zero)
        {
            Vector3 turnToJumpAfterClimb = Vector2.zero;
            Vector3 left = -player.rb.transform.right;
            Vector3 right = player.rb.transform.right;
            Vector3 current = Vector3.zero;
            Vector3 target = Vector3.zero;
            target = player.rb.transform.position;
            target.x += move.x;
            target.z += move.y;

            if (LookAt(Vector3.forward + player.rb.transform.position) == LookAt(target) && ClimbForward())
            {
                player.LockJump = true;
                Vector3 currentCenter = player.rb.transform.position + player.rb.transform.forward + new Vector3(0, 2f, 0);
                player.animations.EndClimb(currentCenter);
            }
            else if (LookAt(target) == LookAt(player.rb.transform.position + Vector3.right))
            {
                if (CheckMoveRight())
                    current = right * move.x * 1;
                else
                {
                    player.LockJump = false;
                    turnToJumpAfterClimb = right * 0.2f;
                    
                }

            }
            else if (LookAt(target) == LookAt(player.rb.transform.position + Vector3.left))
            {
                if (CheckMoveLeft())
                    current = left * -move.x * 1f;
                else
                {
                    player.LockJump = false;
                    turnToJumpAfterClimb = left * 0.2f;
                }
            }
            else if (LookAt(player.rb.transform.position + Vector3.back) == LookAt(target))
            {
                player.LockJump = false;
                turnToJumpAfterClimb = -player.rb.transform.forward * 0.2f;
            }

            player.turnToJumpAfterClimb = new Vector2(turnToJumpAfterClimb.x, turnToJumpAfterClimb.z);
            player.animations.IsClimbing(1);
            player.rb.transform.position += current;

        }
        else
        {
            player.animations.IsClimbing(0);
        }
    }

    public override void Exit()
    {
        if(!player.LockJump)
        {
            player.rb.isKinematic = false;
        }
    }


    private bool CheckMoveRight()
    {
        if (Physics.CheckBox(player.colliderClimb.transform.position + (player.rb.transform.right * player.colliderClimb.size.x), player.colliderClimb.size, Quaternion.identity, player.layerGround))
        {
            return true;
        }
        else { return false; }
    }

    private bool CheckMoveLeft()
    {
        if (Physics.CheckBox(player.colliderClimb.transform.position + (-player.rb.transform.right * player.colliderClimb.size.x), player.colliderClimb.size, Quaternion.identity, player.layerGround))
        {
            return true;
        }
        else { return false; }
    }

    private bool ClimbForward()
    {
        Vector3 currentCenter = player.rb.transform.position + player.rb.transform.forward * player.capsuleCollider.radius * 1.5f + new Vector3(0, 3f, 0);
        Vector3 currentSize = new Vector3(player.capsuleCollider.radius * 1.5f, 1.5f, player.capsuleCollider.radius * 1.5f);
        if (!Physics.CheckBox(currentCenter, currentSize, Quaternion.identity, player.layerGround))
        {
            return true;
        }
        else { return false; }
    }
}
