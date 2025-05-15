using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string _name = "None";
    public Material material;
    public Mesh mesh;
    public List<Effect> effects = new List<Effect>();
    public float duration;
    public AudioClip audioActivate;
    public CreatorEffects creatorEffects;
    public Vector3 scale = Vector3.one;

    public virtual void Activate(Character character, AudioSource audio)
    {
       
    }

}


