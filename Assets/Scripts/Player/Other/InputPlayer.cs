using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputPlayer : MonoBehaviour
{
    [SerializeField] InputActionAsset control;
    [SerializeField] Player player;

    InputAction moveX;
    InputAction moveY;
    InputAction jump;

    private void Start()
    {
        string NameCamera = "PlayerMovement";
        InputActionMap map = control.FindActionMap(NameCamera);
        moveX = map.FindAction("MoveX");
        moveY = map.FindAction("MoveY");
        jump = map.FindAction("Jump");
        moveX.Enable();
        moveY.Enable();
        jump.Enable();

     

    }

    private void Update()
    {
        Move();
        if (jump.WasPressedThisFrame())
        {
            player.Jump();
        }


    }

    private void Move()
    {
        Vector2 current = new Vector2(moveX.ReadValue<float>(), moveY.ReadValue<float>()) * Time.deltaTime;

        player.Move(current);

    }

}
