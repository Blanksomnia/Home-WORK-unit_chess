using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

public class CharacterEntity : MonoBehaviour
{
    TypeUnits type;
    int health = 10;
    int maxHealth = 20;

    [SerializeField]Character charact;

    private float speed;
    public float _speed => speed;
    public int _health => health;
    public int _healthMax => maxHealth;

    private void Awake()
    {
        maxHealth = charact.maxHealth;
        health = charact.maxHealth;
        type = charact.type;
        speed = charact.speed;
        GetComponent<NavMeshAgent>().speed = charact.speed;
    }

    public TypeUnits _type => type;

    public void GetDamage(int dam)
    {
        health -= dam;

        if (health < 0)
        {
            health = 0;
        }

        if (health == 0)
        {
            
        }
    }

    public void ClearHealth()
    {
        health = maxHealth;
    }

}
