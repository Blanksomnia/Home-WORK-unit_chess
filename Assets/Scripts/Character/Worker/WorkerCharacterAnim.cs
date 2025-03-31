using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using Zenject;

public class WorkerCharacterAnim : MonoBehaviour, ICharacterAnimatorListener
{
    Animator animator;
    string walkAnim = "Walk";
    string collectAnim = "stone";
    string stayAnim = "stand";
    string deathAnim = "death";

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }



    public void stay() => Anim(stayAnim);
    public void move()=> Anim(walkAnim);
    public void collect()=> Anim(collectAnim);

    public void attack()=> Anim(stayAnim);
    public void StartDeathAnim()=> Anim(deathAnim);
    private void Anim(string f)
    {
        animator.Play(f);
    }


}
