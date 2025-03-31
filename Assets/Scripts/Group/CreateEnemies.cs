using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class CreateEnemies : MonoBehaviour
{
    ManagerUnits mover;

    [SerializeField] int count = 5;
    [SerializeField] int radius = 5;

    [Inject]
    public void Construct(ManagerUnits manage) { mover = manage;

        mover.CreateUnit(TypeUnits.Enemy, count);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position;
            pos.x += new Random().Next(-radius, radius + 1);
            pos.z += new Random().Next(-radius, radius + 1);
            mover.addUnit(pos, TypeUnits.Enemy);
        }
    }

    
}
