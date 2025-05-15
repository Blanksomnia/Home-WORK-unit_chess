using UnityEngine;


public class Effect : ScriptableObject
{
    public string _name = "";
    public int second = 0;

    public virtual void GetEffect(Character character)
    {
        
    }

    public virtual void EndTimer() 
    {

    }

}



