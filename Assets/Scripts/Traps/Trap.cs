using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] PlayerMemory player;
    [SerializeField] int damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.GetDamage(damage);
        }
    }


}
