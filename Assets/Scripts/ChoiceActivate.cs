using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceActivate : MonoBehaviour
{
    [SerializeField] Idle _idle;
    [SerializeField] Search _search;
    [SerializeField] Collect _collect;
    [SerializeField] Return _return;

    public void Ñhoice(status Status, Transform trans)
    {
        switch(Status)
        {
            case status.Idle: _idle.Activate(); break;
            case status.Search: _search.Activate(); break;
            case status.Collect: _collect.Activate(trans); break;
            case status.Return: _return.Activate(); break;
            default:break;
        }
    }
}
