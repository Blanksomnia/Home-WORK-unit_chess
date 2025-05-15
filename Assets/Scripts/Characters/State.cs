using System;

public abstract class State<T> where T : Enum
{
    public T state;

    public virtual void Enter()
    {
        
    }

    public virtual void Update(float deltaTime) 
    { 

    }

    public virtual void Exit()
    {

    }

}

