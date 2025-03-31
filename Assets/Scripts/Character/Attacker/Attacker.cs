using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : ScriptableObject
{
    public int damage = 2;
    public int duration = 2;
    public float radius = 1;
    public float radiusAttack = 1;
    public LayerMask targets;
    public LayerMask obstacle;
}
