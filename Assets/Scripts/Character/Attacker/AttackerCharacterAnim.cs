using System.Collections;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;
using Zenject;

public class AttackerCharacterAnim : MonoBehaviour, ICharacterAnimatorListener
{
    Animator animator;
    string walkAnim = "Walk";
    string attackAnim = "attack";
    string stayAnim = "stand";
    string deathAnim = "death";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void stay() => Anim(stayAnim);
    public void move() => Anim(walkAnim);
    public void collect() => Anim(stayAnim);
    public void attack() => Anim(attackAnim);
    public void StartDeathAnim() => Anim(deathAnim);

    private void Anim(string f)
    {
        animator.Play(f);
    }



}
