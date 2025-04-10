using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CameraMove : MonoBehaviour
{

    [SerializeField] private float limitMoveCameraHorizontal = 30;
    [SerializeField] private float limitMoveCameraVertical = 5;
    [SerializeField] private float speedMoveHorizontal = 1f;
    [SerializeField] private float speedMoveVertical = 1f;

    InputActionAsset input;
    Camera camera;
    
    private InputAction press;
    private InputAction X;
    private InputAction Y;
    private InputAction Z;

    private float Ymin = 0;
    private float Ymax = 0;
    private float Zmin = 0;
    private float Zmax = 0;
    private float Xmin = 0;
    private float Xmax = 0;

    [Inject]
    public void Construct(InputActionAsset inputs, Camera cam)
    {
        input = inputs;
        camera = cam;
    }

    private void Start()
    {
        Ymax = camera.transform.position.y + limitMoveCameraVertical;
        Ymin = camera.transform.position.y - limitMoveCameraVertical;
        Zmin = camera.transform.position.z - limitMoveCameraHorizontal;
        Zmax = camera.transform.position.z + limitMoveCameraHorizontal;
        Xmin = camera.transform.position.x - limitMoveCameraHorizontal;
        Xmax = camera.transform.position.x + limitMoveCameraHorizontal;

        press = input.FindActionMap("Camera").FindAction("Press");
        Y = input.FindActionMap("Camera").FindAction("Y");  
        X = input.FindActionMap("Camera").FindAction("X");
        Z = input.FindActionMap("Camera").FindAction("Z");
        X.Enable();
        Y.Enable();
        Z.Enable();
        press.Enable();

    }

    private void Update()
    {
        MoveCamera();
       
    }

    private void MoveCamera()
    {     

        if (press.IsPressed())
        {
            Vector3 MOVE = Vector3.zero;
            Vector3 toMove = camera.transform.position;

            toMove.x += -X.ReadValue<float>() * speedMoveHorizontal * Time.deltaTime;
            toMove.z += -Z.ReadValue<float>() * speedMoveHorizontal * Time.deltaTime;
            toMove.y += -Y.ReadValue<float>() * speedMoveVertical * Time.deltaTime;

            if (toMove.x > Xmin && toMove.x < Xmax) { MOVE.x += toMove.x - camera.transform.position.x; } else { }
            if (toMove.z > Zmin && toMove.z < Zmax) { MOVE.z += toMove.z - camera.transform.position.z; } else { }            
            if (toMove.y > Ymin && toMove.y < Ymax) { MOVE.y += toMove.y - camera.transform.position.y; } else { }

            camera.transform.position += MOVE;

        }

    }

}
