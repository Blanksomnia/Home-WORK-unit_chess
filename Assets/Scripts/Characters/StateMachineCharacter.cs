using System;
using System.Collections.Generic;

public class StateMachineCharacter<T> where T : Enum
{
    State<T> _state = null;
    List<State<T>> list = new List<State<T>>();

    public T state { get { return _state.state; } set { GiveState(value); } }

    public void AddState(State<T> st)
    {
        if (CanAdd(st.state))
        {
            if(_state == null)
                _state = st;

            list.Add(st);
        }
    }

    private bool CanAdd(T st)
    {
        if(list.Count == 0)
            return true;
        else
        {
            bool right = true;

            for (int i = 0; i < list.Count; i++)
                if (ID(list[i].state) == ID(st))
                {
                    right = false;
                    break;
                }

            return right;
        }
    }

    private void GiveState(T st)
    {
        if (ID(st) != ID(_state.state))
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (ID(list[i].state) == ID(st))
                {
                    if (_state != null)
                        _state.Exit();

                    _state = list[i];
                    _state.Enter();

                    break;
                }
            }
        }

    }

    private string ID(T st)
    {
        return st.ToString();
    }

    public void Update(float deltaTime)
    {
        if(_state != null)
        _state.Update(deltaTime);
    }

}




