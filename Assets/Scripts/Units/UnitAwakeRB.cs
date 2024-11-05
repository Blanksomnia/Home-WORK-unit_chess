using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAwakeRB : MonoBehaviour
{
    [SerializeField] GameObject UNITS;

    private void Awake()
    {
        new RBKinematic().RBIsKinematic(UNITS, true);
    }
}
