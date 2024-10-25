using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Idle : MonoBehaviour, IStatusVariable
{
    [SerializeField] ChoiceActivate _choiceActivate;
    Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(Stay());
    }
    public void Activate()
    {
        StartCoroutine(Stay());
    }
    IEnumerator Stay()
    {
        _animator.Play("Blend Tree");
        _animator.SetFloat("RUN", 0);
        yield return new WaitForSeconds(5);
        _choiceActivate.Ñhoice(ChooseStatus(), null);
    }

    public status ChooseStatus()
    {
        return status.Search;
    }

}
