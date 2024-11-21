using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeCycleLight : MonoBehaviour
{
    [SerializeField] private float Time = 5;

    private void Update()
    {

        transform.Rotate(Time * UnityEngine.Time.deltaTime, 0, 0);
    }

}
