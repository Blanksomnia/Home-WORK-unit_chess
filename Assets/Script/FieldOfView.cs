using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] public float radius;
    public Transform Target;
    public Vector3 reviewCenter;
    [HideInInspector] public bool _canSeePlayer;

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position + reviewCenter, Target.position);
        if (distanceToTarget <= radius)
        {
            _canSeePlayer = true;
        }
        else
        {
            _canSeePlayer = false;
        }
    }

}
