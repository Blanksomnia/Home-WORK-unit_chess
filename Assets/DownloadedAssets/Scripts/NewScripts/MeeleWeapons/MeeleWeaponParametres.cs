using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeaponParametres : MonoBehaviour
{
    [SerializeField] int Damage = 10;
    public string Name = "Meele weapon";
    public int _damage => Damage;
}
