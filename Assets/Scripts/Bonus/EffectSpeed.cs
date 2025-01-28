using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class EffectSpeed : Bonus
{
    MeshRenderer renderer;
    public float speed = 1f;

    private void Awake()
    {

        renderer = GetComponent<MeshRenderer>();
    }

    public override void Detect()
    {
        player.GetSpeed(speed);
        renderer.enabled = false;

    }
}
