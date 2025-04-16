
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementPlayer
{

    Movement mover;
    public StatePlayer state {  get { return mover.state; } set { GiveState(value); } }

    List<Movement> list = new List<Movement>();

    public void AddState(Movement _mover)
    {
        if (CanAdd(_mover.state))
        {
            if(mover == null)
            {
                mover = _mover;
            }

            list.Add(_mover);
        }

    }

    private bool CanAdd(StatePlayer _state)
    {
        if(list.Count == 0)
        {
            return true;
        }
        else
        {
            bool right = true;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].state == _state)
                {
                    right = false; 
                }
            }
            return right;
        }
    }

    private void GiveState(StatePlayer _state)
    {
        if (_state != mover.state)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].state == _state)
                {
                    Debug.Log(_state);
                    if (mover != null)
                    mover.Exit();

                    mover = list[i];
                    mover.Enter();

                    break;
                }
            }
        }

    }

    public void Update()
    {
        if(mover != null)
        mover.Update();
    }

}



