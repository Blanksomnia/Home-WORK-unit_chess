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

    private Vector3 toMove;
    private Vector3 previousFrame;


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
            toMove = camera.transform.position;
            previousFrame = toMove;

            toMove.x += -X.ReadValue<float>() * speedMoveHorizontal * Time.deltaTime;
            toMove.z += -Z.ReadValue<float>() * speedMoveHorizontal * Time.deltaTime;
            toMove.y += -Y.ReadValue<float>() * speedMoveVertical * Time.deltaTime;

            if (toMove.x > Xmin && toMove.x < Xmax) { } else { toMove.x = previousFrame.x; }
            if (toMove.z > Zmin && toMove.z < Zmax) { } else { toMove.z = previousFrame.z; }            
            if (toMove.y > Ymin && toMove.y < Ymax) { } else { toMove.y = previousFrame.y; }

            camera.transform.position = toMove;
        }

    }

}
