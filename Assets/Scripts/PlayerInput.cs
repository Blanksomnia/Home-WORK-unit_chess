using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public bool IsPressed = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            IsPressed = false;
        }
    }
}
