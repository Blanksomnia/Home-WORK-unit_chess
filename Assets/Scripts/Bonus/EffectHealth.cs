using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHealth : Bonus
{
    MeshRenderer renderer;
    
    public int health = 30;

    private void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    public override void Detect()
    {

        player.GetHealth(health);
        renderer.enabled = false;

    }
}
