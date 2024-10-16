using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] bool Left;
    [SerializeField] bool Right;
    [SerializeField] bool Forward;
    [SerializeField] private LayerMask Wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            if (Left)
            {
                fieldOfView.Left = true;
            }
            if (Right)
            {
                fieldOfView.Right = true;
            }
            if (Forward)
            {
                fieldOfView.Forward = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if (Left)
            {
                fieldOfView.Left = false;
            }
            if (Right)
            {
                fieldOfView.Right = false;
            }
            if (Forward)
            {
                fieldOfView.Forward = false;
            }
        }
    }

}
