using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCharacter : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] int maxhealth = 1;
    public int _health { get { return health; } set { health = CheckHealth(value);  } }

    private int CheckHealth(int health)
    {
        int current = health;

        if(health > maxhealth)
        {
            current = maxhealth;
        }

        if(health < 0)
        {
            current = 0;
        }

        return current;
    }

}
